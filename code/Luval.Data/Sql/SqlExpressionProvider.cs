using Luval.Data.Extensions;
using Luval.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Luval.Data.Sql
{
    /// <summary>
    /// Represents an implementation of <see cref="ISqlExpressionProvider"/> for Ansi SQL commands
    /// </summary>
    public class SqlExpressionProvider : ISqlExpressionProvider
    {

        #region Variable Declaration

        private DbTableSchema _schema;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="SqlExpressionProvider"/>
        /// </summary>
        /// <param name="schema">The <see cref="DbTableSchema"/> to use in the implementation</param>
        public SqlExpressionProvider(DbTableSchema schema)
        {
            _schema = schema;
        }

        #endregion

        #region Interface Members

        /// <inheritdoc/>
        public string ResolveWhere<T>(Expression<Func<T, bool>> expression)
        {
            return ResolveExpression(expression.Body, typeof(T));
        }

        /// <inheritdoc/>
        public string ResolveOrderBy<T>(Expression<Func<T, object>> orderBy, bool descending)
        {
            if (orderBy == null) return string.Empty;
            var expression = (UnaryExpression)orderBy.Body;
            var memberExpression = (MemberExpression)expression.Operand;
            var column = ResolveColumnName(memberExpression.Member.Name);
            return "ORDER BY {0} {1}".Fi(ResolveColumnName(column), descending ? "DESC" : "ASC");
        }

        #endregion

        #region Expresion Methods

        private string ResolveBinaryExpression(Expression expression, Type modelType)
        {
            var localExpression = (BinaryExpression)expression;
            var left = ResolveExpression(localExpression.Left, modelType);
            var right = ResolveExpression(localExpression.Right, modelType);
            var oper = ResolveExpressionNodeType(localExpression.NodeType);
            return string.Format("({0} {1} {2})", left, oper, right);
        }

        private static bool IsConstantExpression(Type type)
        {
            return typeof(ConstantExpression) == type || type.IsSubclassOf(typeof(ConstantExpression));
        }

        private string ResolveExpression(Expression expression, Type modelType)
        {
            var type = expression.GetType();
            if (typeof(MemberExpression) == type || type.IsSubclassOf(typeof(MemberExpression)))
                return ResolveMemberExpression((MemberExpression)expression, modelType);
            if (IsConstantExpression(type))
                return ResolveConstantExpression(expression);
            if (typeof(MethodCallExpression) == type || type.IsSubclassOf(typeof(MethodCallExpression)))
                return ResolveMethodExpression(expression);
            if (typeof(BinaryExpression) == type || type.IsSubclassOf(typeof(BinaryExpression)))
                return ResolveBinaryExpression(expression, modelType);
            throw new ArgumentException(string.Format("Expression type {0} is not supported", type));
        }

        private string ResolveMethodExpression(Expression expression)
        {
            var localExpression = (MethodCallExpression)expression;
            var memberExpression = (MemberExpression)localExpression.Object;
            object value = null;
            if(memberExpression.Expression != null)
                value = GetConstantValueFromExpression(memberExpression.Expression);
            else
            {
                throw new InvalidOperationException("Expression not supported {0}".Fi(expression));
            }
            return Convert.ToString(localExpression.Method.Invoke(value, null)).ToSql();
        }

        private string ResolveMemberExpression(MemberExpression expression, Type modelType)
        {
            if (expression.Expression != null && expression.Expression.NodeType == ExpressionType.Parameter ||
                expression.Expression.NodeType == ExpressionType.Convert)
            {
                var propertyInfo = (PropertyInfo)expression.Member;
                return ResolveColumnName(propertyInfo.Name);
            }
            var member = Expression.Convert(expression, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var getter = lambda.Compile();
            return getter().ToSql();
        }

        private string ResolveConstantExpression(Expression expression)
        {
            return GetConstantValueFromExpression(expression).ToSql();
        }

        private object GetConstantValueFromExpression(Expression expression)
        {
            var localExpression = (ConstantExpression)expression;
            var member = Expression.Convert(localExpression, typeof(object));
            var lambda = Expression.Lambda<Func<object>>(member);
            var getter = lambda.Compile();
            return getter();
        }

        private string ResolveExpressionNodeType(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.AndAlso:
                    return "And";
                case ExpressionType.And:
                    return "And";
                case ExpressionType.OrElse:
                    return "Or";
                case ExpressionType.Or:
                    return "Or";
                case ExpressionType.NotEqual:
                    return "<>";
            }
            throw new ArgumentException(string.Format("ExpressionType {0} not supported", nodeType));
        }

        #endregion

        #region Helper Methods
        
        private string ResolveColumnName(string propertyName)
        {
            return _schema.Columns.Where(i => i.PropertyName == propertyName).First().ColumnName;
        } 

        #endregion
    }

    /// <summary>
    /// Represents an implementation of <see cref="ISqlExpressionProvider"/> for Ansi SQL commands
    /// </summary>
    /// <typeparam name="TEntity">The entity <see cref="Type"/> to use</typeparam>
    public class SqlExpressionProvider<TEntity> : SqlExpressionProvider
    {
        /// <summary>
        /// Creates a new instance of <see cref="SqlExpressionProvider{TEntity}"/>
        /// </summary>
        public SqlExpressionProvider() : base(DbTableSchema.Create(typeof(TEntity)))
        {

        }
    }
}
