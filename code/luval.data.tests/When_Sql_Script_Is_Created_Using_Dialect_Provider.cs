using Luval.Data.tests.Mocks.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luval.Data.tests
{
    [TestFixture]
    public class When_Sql_Script_Is_Created_Using_Dialect_Provider
    {
        [Test]
        public void It_Should_Properly_Generate_A_Read_Statement()
        {
            var entity = new Complex()
            {
                Id = new Random().Next(),
                Name = "Sample Text",
                Value = new Random().NextDouble(),
                UpdatedOn = DateTime.Today,
                DoNotMap = "Do Not Map",
                DoNotMap2 = "Do Not Map2"
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetReadCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("SELECT [ComplexId], [ComplexName], [ComplexValue], [ComplexUpdatedOn] FROM [ComplextTable] WHERE [ComplexId] = {0};", entity.Id)
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }

        [Test]
        public void It_Should_Properly_Generate_A_Create_Statement()
        {
            var entity = new Complex()
            {
                Id = new Random().Next(),
                Name = "Sample Text",
                Value = new Random().NextDouble(),
                UpdatedOn = DateTime.Today,
                DoNotMap = "Do Not Map",
                DoNotMap2 = "Do Not Map2"
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetCreateCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("INSERT ([ComplexName], [ComplexValue], [ComplexUpdatedOn]) INTO [ComplextTable]  VALUES({0}, {1}, {2});",
                entity.Name.ToSql(), entity.Value.ToSql(), entity.UpdatedOn.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }

        [Test]
        public void It_Should_Properly_Generate_A_Update_Statement()
        {
            var entity = new Complex()
            {
                Id = new Random().Next(),
                Name = "Sample Text",
                Value = new Random().NextDouble(),
                UpdatedOn = DateTime.Today,
                DoNotMap = "Do Not Map",
                DoNotMap2 = "Do Not Map2"
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetUpdateCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("UPDATE [ComplextTable] SET [ComplexName] = {0}, [ComplexValue] = {1}, [ComplexUpdatedOn] = {2} WHERE [ComplexId] = {3};",
                entity.Name.ToSql(), entity.Value.ToSql(), entity.UpdatedOn.ToSql(), entity.Id.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }

        [Test]
        public void It_Should_Properly_Generate_A_Delete_Statement()
        {
            var entity = new Complex()
            {
                Id = new Random().Next(),
                Name = "Sample Text",
                Value = new Random().NextDouble(),
                UpdatedOn = DateTime.Today,
                DoNotMap = "Do Not Map",
                DoNotMap2 = "Do Not Map2"
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetDeleteCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("DELETE FROM [ComplextTable] WHERE [ComplexId] = {3};",
                entity.Name.ToSql(), entity.Value.ToSql(), entity.UpdatedOn.ToSql(), entity.Id.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }

        [Test]
        public void It_Should_Properly_Generate_A_Statement_With_Multiple_Primary_Keys()
        {
            var entity = new MultipleKeys()
            {
                IntKey = new Random().Next(),
                StrKey = Guid.NewGuid().ToString(),
                Data = Guid.NewGuid().ToString()
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetDeleteCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("DELETE FROM [MultipleKeys] WHERE [IntKey] = {0} AND [StrKey] = {1};",
                entity.IntKey.ToSql(), entity.StrKey.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }

        [Test]
        public void It_Should_Properly_Generate_A_Statement_With_A_Where_Clause_For_Null_Values()
        {
            var entity = new MultipleKeys()
            {
                IntKey = DBNull.Value,
                StrKey = null,
                Data = Guid.NewGuid().ToString()
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetDeleteCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("DELETE FROM [MultipleKeys] WHERE [IntKey] IS NULL AND [StrKey] IS NULL;")
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);

        }

        [Test]
        public void It_Should_Properly_Generate_An_Update_Statement_With_A_Null_Value()
        {
            var entity = new MultipleKeys()
            {
                IntKey = new Random().Next(),
                StrKey = null,
                Data = null
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetUpdateCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("UPDATE [MultipleKeys] SET [Data] = NULL WHERE [IntKey] = {0} AND [StrKey] IS NULL;",
                entity.IntKey.ToSql(), entity.StrKey.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);
        }

        [Test]
        public void It_Should_Ignore_NotMapped_Attributes_From_Update_Statement()
        {
            var entity = new MultipleKeys()
            {
                IntKey = new Random().Next(),
                StrKey = null,
                Data = null,
                DoNotMap = "Testing"
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetUpdateCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("UPDATE [MultipleKeys] SET [Data] = NULL WHERE [IntKey] = {0} AND [StrKey] IS NULL;",
                entity.IntKey.ToSql(), entity.StrKey.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);
        }

        [Test]
        public void It_Should_Ignore_NotMapped_Attributes_From_Read_Statement()
        {
            var entity = new MultipleKeys()
            {
                IntKey = new Random().Next(),
                StrKey = null,
                Data = null,
                DoNotMap = "Testing"
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetReadCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("SELECT [IntKey], [StrKey], [Data] FROM  [MultipleKeys] WHERE [IntKey] = {0} AND [StrKey] IS NULL;",
                entity.IntKey.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);
        }

        [Test]
        public void It_Should_Properly_Generate_A_Read_All_Rows_Statement()
        {
            var entity = new MultipleKeys()
            {
                IntKey = new Random().Next(),
                StrKey = null,
                Data = null,
                DoNotMap = "Testing"
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetReadAllCommand().Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("SELECT [IntKey], [StrKey], [Data] FROM [MultipleKeys];",
                entity.IntKey.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);
        }

        [Test]
        public void It_Should_Ignore_NotMapped_Attributes_From_Create_Statement()
        {
            var entity = new MultipleKeys()
            {
                IntKey = new Random().Next(),
                StrKey = "kEY",
                Data = "DATA",
                DoNotMap = "Testing"
            };
            var provider = new SqlServerDialectProvider(SqlTableSchema.Load(entity.GetType()));
            var statement = provider.GetCreateCommand(DictionaryDataRecord.FromEntity(entity)).Trim().ToLowerInvariant().Replace(" ", "");
            var expected = string.Format("INSERT ([IntKey], [StrKey], [Data]) INTO [MultipleKeys] VALUES ({0},{1},{2});",
                entity.IntKey.ToSql(), entity.StrKey.ToSql(), entity.Data.ToSql())
                    .Trim().ToLowerInvariant().Replace(" ", "");
            Assert.AreEqual(expected, statement);
        }
    }
}
