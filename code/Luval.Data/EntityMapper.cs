﻿using Luval.Data.Attributes;
using Luval.Data.Extensions;
using Luval.Data.Sql;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Luval.Data
{
    /// <summary>
    /// Provides methods to map entities
    /// </summary>
    public static class EntityMapper
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> _mappedValues = new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();
        private static readonly ConcurrentDictionary<Type, EntityMetadata> _entityMetadata = new ConcurrentDictionary<Type, EntityMetadata>();

        /// <summary>
        /// Creates and loads the data into a new entity
        /// </summary>
        /// <typeparam name="TEntity">The entity <see cref="Type"/> to work with</typeparam>
        /// <param name="record">The <see cref="IDataRecord"/> containing the data</param>
        /// <returns>A new typed entity</returns>
        public static TEntity FromDataRecord<TEntity>(IDataRecord record)
        {
            return (TEntity)Convert.ChangeType(FromDataRecord(record, typeof(TEntity)), typeof(TEntity));
        }

        /// <summary>
        /// Creates and loads the data into a new entity
        /// </summary>
        /// <param name="record">The <see cref="IDataRecord"/> containing the data</param>
        /// <param name="entityType">The <see cref="Type"/> of the entity to create</param>
        /// <returns>A new object entity</returns>
        public static object FromDataRecord(IDataRecord record, Type entityType)
        {
            var entity = Activator.CreateInstance(entityType);
            for (int i = 0; i < record.FieldCount; i++)
            {
                AssignFieldValueToEntity(record.GetName(i), ref entity, record.GetValue(i));
            }
            return entity;
        }

        /// <summary>
        /// Creates a new <see cref="IDataRecord"/> from an entity
        /// </summary>
        /// <param name="entity">The entity <see cref="object"/> to extract the data from</param>
        /// <returns></returns>
        public static IDataRecord ToDataRecord(object entity)
        {
            return new DictionaryDataRecord(ToDictionary(entity));
        }

        private static void AssignFieldValueToEntity(string fieldName, ref object entity, object value)
        {
            var p = GetEntityPropertyFromFieldName(fieldName, entity.GetType());
            if (p == null) return;
            if (DBNull.Value == value || value == null) value = GetDefaultValue(p.PropertyType);
            var typeToConvert = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;
            p.SetValue(entity, TryChangeType(value, typeToConvert));
        }

        private static object TryChangeType(object val, Type type)
        {
            try
            {
                val = Convert.ChangeType(val, type);
            }
            catch (InvalidCastException)
            {
                if (val != null && (typeof(Guid) == val.GetType()))
                    val = ((Guid)val).ToString();
            }
            catch (Exception)
            {
            }
            return val;
        }

        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        private static PropertyInfo GetEntityPropertyFromFieldName(string name, Type type)
        {
            if (_mappedValues.ContainsKey(type) && _mappedValues[type].ContainsKey(name))
                return _mappedValues[type][name];

            PropertyInfo property;
            property = type.GetProperty(name);
            if (property == null)
            {
                foreach (var prop in type.GetProperties())
                {
                    var columnName = prop.GetCustomAttribute<ColumnNameAttribute>();
                    if (columnName == null) continue;
                    if (((ColumnNameAttribute)columnName).Name == name)
                    {
                        property = prop;
                        break;
                    }
                }
                if (property != null && property.GetCustomAttribute<NotMappedAttribute>() != null)
                    property = null;
            }
            else
            {
                if (property.GetCustomAttribute<NotMappedAttribute>() != null)
                    property = null;
            }
            CreateOrUpdateMapEntry(type, name, property);
            return property;
        }

        private static void CreateOrUpdateMapEntry(Type type, string filedName, PropertyInfo property)
        {
            if(!_mappedValues.ContainsKey(type))
            {
                _mappedValues.TryAdd(type, new Dictionary<string, PropertyInfo>());
                if (!_mappedValues[type].ContainsKey(filedName))
                    _mappedValues[type].Add(filedName, property);
            }
            _mappedValues[type][filedName] = property;
        }

        private static EntityMetadata GetEntityMetadata(Type entityType)
        {
            if (_entityMetadata.ContainsKey(entityType)) return _entityMetadata[entityType];

            var metaData = new EntityMetadata(entityType);
            foreach (var property in entityType.GetProperties())
            {
                var field = new EntityFieldMetadata
                {
                    Property = property,
                    IsMapped = !(property.GetCustomAttribute<NotMappedAttribute>() != null),
                    IsPrimitive = ObjectExtensions.IsPrimitiveType(property.PropertyType)
                };
                var colAtt = property.GetCustomAttribute<ColumnNameAttribute>();
                field.DataFieldName = colAtt != null ? colAtt.Name : property.Name;
                if (!field.IsPrimitive)
                {
                    field.IsList = typeof(IEnumerable).IsAssignableFrom(property.PropertyType);
                    //var refTable = property.GetCustomAttribute<TableReferenceAttribute>();
                    //if (refTable == null) refTable = new TableReferenceAttribute();
                    field.TableReference = TableReference.Create(property);
                    DbTableSchema.ValidateTableRef(field.TableReference, DbTableSchema.Create(entityType));
                }
                metaData.Fields.Add(field);
            }
            _entityMetadata[entityType] = metaData;
            return metaData;
        }

        private static Dictionary<string, object> ToDictionary(object entity)
        {
            var type = entity.GetType();
            if (typeof(IDictionary<string, object>).IsAssignableFrom(type))
                return (Dictionary<string, object>)entity;
            if (typeof(IDataRecord).IsAssignableFrom(type))
                return (Dictionary<string, object>)((IDataRecord)entity).ToDictionary();

            var metaData = GetEntityMetadata(type);
            var record = new Dictionary<string, object>();
            foreach (var field in metaData.Fields)
            {
                if (!field.IsMapped) continue;
                var value = field.Property.GetValue(entity);
                if (field.IsPrimitive)
                    record[field.DataFieldName] = value;
                else
                {
                    if (field.IsList)
                    {
                        if(!value.IsNullOrDbNull())
                        {
                            var list = new List<IDataRecord>();
                            foreach (var item in (IEnumerable)value)
                                list.Add(ToDataRecord(item));
                            record[field.DataFieldName] = list;
                        }
                    }
                    else
                    {
                        if (field.TableReference != null && !record.ContainsKey(field.TableReference.ReferenceTableKey))
                        {
                            var parentMetaData = GetEntityMetadata(field.TableReference.EntityType);
                            var parentField = parentMetaData.Fields.FirstOrDefault(i => i.DataFieldName == field.TableReference.ReferenceTable.Columns.Where(c => c.IsPrimaryKey).First().ColumnName);
                            if (parentField != null)
                                record[field.TableReference.ReferenceTableKey] = parentField.Property.GetValue(value);
                        }
                    }
                }
            }
            return record;
        }
    }
}
