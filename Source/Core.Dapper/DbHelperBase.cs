using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace OppenIT.Core.Dapper
{
    public class DbHelperBase : IDisposable
    {
        public DbHelperBase()
        {

        }
        protected string DBProviderName { get; set; }
        protected string ConnectionString { get; set; }
        public DbHelperBase(string dbProviderName, string connectionString)
        {
            Init(dbProviderName, connectionString);
        }
        private DbProviderFactory _dbFactory = null;
        protected DbProviderFactory DbFactory
        {
            get 
            { 
                if (_dbFactory == null)
                    _dbFactory = DbProviderFactories.GetFactory(this.DBProviderName);
                return _dbFactory; 
            }
        }
        private IDbTransaction _Transaction = null;
        /// <summary>
        /// 使用的事务
        /// </summary>
        public IDbTransaction Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }

        private IDbConnection _conn = null;
        /// <summary>
        /// 使用的连接
        /// </summary>
        public IDbConnection Connection
        {
            get 
            {
                if (_conn == null)
                { 
                    _conn = this.DbFactory.CreateConnection();
                    _conn.ConnectionString = this.ConnectionString;
                }
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();
                return _conn; 
            }
        }
        public void Init(string dbProviderName, string connectionString)
        {
            this.DBProviderName = dbProviderName;
            this.ConnectionString = connectionString;
            _dbFactory = DbProviderFactories.GetFactory(dbProviderName);
            _conn = _dbFactory.CreateConnection();
            _conn.ConnectionString = connectionString;
        }
        public TKey Insert<TKey>(object insertEntity)
        {
            return this.Connection.Insert<TKey>(insertEntity, this.Transaction);
        }
        public void Insert(object insertEntity)
        {
            this.Connection.Insert(insertEntity, this.Transaction);
        }
        public void Delete(object deleteEntity)
        {
            this.Connection.Delete(deleteEntity, this.Transaction);
        }
        public void Update(object updateEntity)
        {
            this.Connection.Update(updateEntity, this.Transaction);
        }
        public List<T> GetList<T>(object whereConditions)
        {
            return this.Connection.GetList<T>(whereConditions, this.Transaction).ToList();
        }
        public List<T> GetList<T>()
        {
            return this.Connection.GetList<T>().ToList();
        }
        public List<T> GetList<T>(string conditions, object parameters = null)
        {
            return this.Connection.GetList<T>(conditions, parameters, this.Transaction).ToList();
        }
        public T Get<T>(string conditions, object parameters = null)
        {
            return this.Connection.GetList<T>(conditions, parameters, this.Transaction).FirstOrDefault();
        }
        public T Get<T>(object whereConditions)
        {
            return this.Connection.GetList<T>(whereConditions, this.Transaction).FirstOrDefault();
        }
        public T GetByID<T>(object id)
        {
            return this.Connection.Get<T>(id,this.Transaction);
        }
        public List<T> Query<T>(string sql, object param = null)
        {
            return this.Connection.Query<T>(sql,param,this.Transaction).ToList();
        }
        public T QueryFirst<T>(string sql, object param = null)
        {
            return this.Connection.QueryFirstOrDefault<T>(sql, param, this.Transaction);
        }
        public SqlMapper.GridReader QueryMultiple(string sql, object param = null)
        {
            return this.Connection.QueryMultiple(sql, param, this.Transaction);
        }
        public int Execute(string sql, object param = null)
        {
            return this.Connection.Execute(sql, param, this.Transaction);
        }
        public object ExecuteScalar(string sql, object param = null)
        {
            return this.Connection.ExecuteScalar(sql, param, this.Transaction);
        }
        public void BeginTransaction()
        {
            _Transaction = this.Connection.BeginTransaction();
        }
        public void Commit()
        {
            if (_Transaction == null) return;

            _Transaction.Commit();
            _Transaction = null;
        }
        public void Rollback()
        {
            if (_Transaction == null) return;

            _Transaction.Rollback();
            _Transaction = null;
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (_conn != null && _conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }
        #region IDisposable interfaces
        /// <summary>
        ///		Free the instance variables of this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources.
                if (_conn != null && _conn.State == ConnectionState.Open) _conn.Close();

                _conn = null;
                _Transaction = null;
            }

            // Clean up unmanaged resources
        }

        /// <summary>
        ///     Dispose of this object's resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // as a service to those who might inherit from us
        }

        ~DbHelperBase()
        {
            Dispose(false);
        }
        #endregion
    }
}
