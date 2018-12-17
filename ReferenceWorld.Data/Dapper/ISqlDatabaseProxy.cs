using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace ReferenceWorld.Data.Dapper
{
    public interface ISqlDatabaseProxy
    {
        IEnumerable<T> Query<T>(string sql);
        IEnumerable<T> Query<T>(string sql, object param);
        int Execute<T>(string sql, params T[] items);
        int Execute(string sql);
        int Insert<T>(string sql, T item);
        T QueryMulti<T>(string sql, object param, Func<SqlMapper.GridReader, T> fill);
        void InsertList(string sql, object list);
        IDbConnection Connection { get; }
    }
}
