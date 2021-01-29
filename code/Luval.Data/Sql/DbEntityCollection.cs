using Luval.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Luval.Data.Sql
{
    public class DbEntityCollection<TEntity, TKey> : EntityCollection<TEntity, TKey>
    {
        private IQuery<TEntity, TKey> _query;
        public DbEntityCollection(Database database, IDbDialectProvider sqlDialectProvider)
        {
            _query = new DbQuery<TEntity, TKey>(database, sqlDialectProvider);
        }

        public override IQuery<TEntity, TKey> Query { get { return _query; } }

    }
}
