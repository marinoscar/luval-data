using Luval.Data.Sql;
using System;
using System.Data;
using System.Linq.Expressions;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Identifies an implementation that can create sql commands
    /// </summary>
    public interface IDbDialectProvider
    {
        /// <summary>
        /// Gets the <see cref="DbTableSchema"/> object for the <see cref="IDbDialectProvider"/>
        /// </summary>
        DbTableSchema Schema { get; }
        /// <summary>
        /// Gets a sql statement to create a new record in the database
        /// </summary>
        /// <param name="record">The <see cref="IDataRecord"/> that contains the information to insert</param>
        /// <param name="incudeChildren">Indicates if in the case of a complex data type to also insert the children entities</param>
        /// <returns>A sql comment <see cref="string"/></returns>
        string GetCreateCommand(IDataRecord record, bool incudeChildren);
        /// <summary>
        /// Gets a sql statement to select a record in the database
        /// </summary>
        /// <param name="record">The <see cref="IDataRecord"/> that contains the information to insert</param>
        /// <returns>A sql comment <see cref="string"/></returns>
        string GetReadCommand(IDataRecord record);
        /// <summary>
        /// Gets a sql statement to update a record in the database
        /// </summary>
        /// <param name="record">The <see cref="IDataRecord"/> that contains the information to insert</param>
        /// <returns>A sql comment <see cref="string"/></returns>
        string GetUpdateCommand(IDataRecord record);
        /// <summary>
        /// Gets a sql statement to delete a record in the database
        /// </summary>
        /// <param name="record">The <see cref="IDataRecord"/> that contains the information to insert</param>
        /// <returns>A sql comment <see cref="string"/></returns>
        string GetDeleteCommand(IDataRecord record);
        /// <summary>
        /// Gets a sql select sql to retrieve records from the database
        /// </summary>
        /// <param name="expression">A lamda expresion of the data to extract</param>
        /// <returns>A sql comment <see cref="string"/></returns>
        string GetEntityQuery<TEntity>(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// Creates a sql command to get all of the records from a table
        /// </summary>
        /// <returns>A sql comment <see cref="string"/></returns>
        string GetReadAllCommand();
    }
}