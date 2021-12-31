using Luval.Data.Interfaces;
using Luval.Data.Sql;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Luval.Data.Extensions
{
    /// <summary>
    /// Provide extension methods for the <see cref="string"/> data type
    /// </summary>
    public static class StringExtensions
    {
        public static readonly Char[] SqlLikeChars = new[] { '%', '_', '[', ']' };

        /// <summary>
        /// Provides a <seealso cref="CultureInfo.InvariantCulture"/> formatted <see cref="string"/> value
        /// </summary>
        /// <param name="format">A <see cref="string"/> with the format</param>
        /// <param name="args">Arguments to use</param>
        /// <returns>Formatted string</returns>
        public static string FormatInvariant(this string format, params object[] args)
        {
            return 0 == args.Length ? format : string.Format(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Formats a string to be used as part of a SQL statement with a <see cref="SqlFormatter"/> implementation
        /// </summary>
        /// <param name="format">A <see cref="string"/> with the format</param>
        /// <param name="args">Arguments to use</param>
        /// <returns>Sql formatted string</returns>
        public static string FormatSql(this string format, params object[] args)
        {
            return string.Format(SqlFormatter.Instance, format, args);
        }

        /// <summary>
        /// Alias for <see cref="FormatInvariant" />.
        /// </summary>
        /// <param name="format">A <see cref="string"/> with the format</param>
        /// <param name="args">Arguments to use</param>
        /// <returns>Formatted string</returns>
        public static string Fi(this string format, params object[] args)
        {
            return FormatInvariant(format, args);
        }

        /// <summary>
        /// Verifies the proper implementation of sql like characters
        /// </summary>
        /// <param name="s">input string to use</param>
        /// <returns>A escaped sql string</returns>
        public static string EscapeSqlLikeChars(this string s)
        {
            // make the common case fast
            if (-1 == s.LastIndexOfAny(SqlLikeChars))
            {
                return s;
            }

            for (var i = 0; i < SqlLikeChars.Length; i++)
            {
                var c = SqlLikeChars[i];
                if (s.LastIndexOf(c) != -1)
                {
                    s = s.Replace(c.ToString(), "[" + c + "]");
                }
            }

            return s;
        }

        /// <summary>
        /// Splits the string into an string array of different sizes
        /// </summary>
        /// <param name="s">string to work on</param>
        /// <param name="itemSize">The size of the new string</param>
        /// <returns>IEnumerable with the new string</returns>
        public static IEnumerable<string> Split(this string s, int itemSize)
        {
            if (s.Length <= itemSize) return new[] { s };
            return Enumerable.Range(0, s.Length / itemSize).Select(i => s.Substring(i * itemSize, itemSize));
        }

        /// <summary>
        /// Converts the string to a <see cref="IQueryCommand"/> instance
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IQueryCommand ToCmd(this string s)
        {
            return new SqlQueryCommand(s);
        }

    }
}
