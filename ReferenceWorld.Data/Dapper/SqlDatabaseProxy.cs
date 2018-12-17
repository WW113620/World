using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using Dapper;

namespace ReferenceWorld.Data.Dapper
{
    public class SqlDatabaseProxy : ISqlDatabaseProxy, IDisposable
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private IDbConnection _connection;

        public SqlDatabaseProxy(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;

        }

        public IEnumerable<T> Query<T>(string sql)
        {
            EnsureConnection();

            IEnumerable<T> query = _connection.Query<T>(sql);
            return query.ToList();
        }

        public IEnumerable<T> Query<T>(string sql, object param)
        {
            try
            {
                EnsureConnection();
                return _connection.Query<T>(sql, param);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int Execute<T>(string sql, params T[] items)
        {
            EnsureConnection();
            return _connection.Execute(sql, items);
        }

        public int Execute(string sql)
        {
            EnsureConnection();
            return _connection.Execute(sql);
        }

        public int Insert<T>(string sql, T item)
        {
            EnsureConnection();
            return _connection.Execute(sql, item);
        }

        public void InsertList(string sql, object list)
        {
            EnsureConnection();
            _connection.Execute(sql, list);
        }

        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public void EnsureConnection()
        {
            if (_connection == null || _connection.State != ConnectionState.Open)
            {
                try
                {
                    _connection = _dbConnectionFactory.CreateConnection();
                }
                catch (Exception)
                {
                    throw new Exception("Failed to create connection");
                }
            }
        }

        public T QueryMulti<T>(string sql, object param, Func<SqlMapper.GridReader, T> fill)
        {
            T t;
            EnsureConnection();
            using (var muti = _connection.QueryMultiple(sql, param))
            {
                t = fill(muti);
            }

            return t;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
