using System;
using System.Collections.Generic;
using System.Text;

namespace Luval.Data.Interfaces
{
    /// <summary>
    /// Interface to execute a query command on a data storage
    /// </summary>
    public interface IQueryCommand
    {
        /// <summary>
        /// Gets the command to executre
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> with the expected command</typeparam>
        /// <returns></returns>
        T Get<T>();
    }
}
