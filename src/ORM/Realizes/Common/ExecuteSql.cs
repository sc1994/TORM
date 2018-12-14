using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ORM.Realizes
{
    /// <summary>
    /// sql 执行
    /// </summary>
    /// <typeparam name="T"></typeparam>
    // ReSharper disable once UnusedTypeParameter
    public partial class RealizeCommon<T>
    {
        /// <summary>
        /// 执行sql，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected long Execute(string sql, Transaction transaction = null, object param = null)
        {
            _explainSpan = DateTime.Now - _starTime;
            _starTime = DateTime.Now;
            // 决定是使用默认参数还是传入参数
            var thatParam = param ?? _params;
            var isError = false;
            try
            {
                // 事务操作
                if (transaction != null)
                {
                    var value = Stores.ConnectionDic[transaction.Sole];
                    if (value.Transaction == null) // 希望在 事务开始的时候尽量少的参数，所以连接的开启放在了这边
                    {
                        value.Connection.ConnectionString = GetTableInfo().ConnectionString;
                        value.Connection.Open();
                        value.Transaction = value.Connection.BeginTransaction(); // 涉及到对字典中的值进行变动，不能预知当高并发的情况下，是否会产生问题。
                    }
                    _connSpan = DateTime.Now - _starTime;
                    _starTime = DateTime.Now;
                    return value.Connection.Execute(sql, thatParam, Stores.ConnectionDic[transaction.Sole].Transaction);
                }
                // 非事务
                MySqlConnection connection;
                using (connection = new MySqlConnection(GetTableInfo().ConnectionString))
                {
                    return connection.Execute(sql, thatParam);
                }
            }
            catch (Exception ex)
            {
                LogSql(sql, thatParam, ex);
                isError = true;
                throw;
            }
            finally
            {
                if (!isError) LogSql(sql, thatParam);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected TOther QueryFirst<TOther>(string sql, object param = null)
        {
            _explainSpan = DateTime.Now - _starTime;
            _starTime = DateTime.Now;
            var thatParam = param ?? _params;
            var isError = false;
            try
            {
                using (var connection = new MySqlConnection(GetTableInfo().ConnectionString))
                {
                    _connSpan = DateTime.Now - _starTime;
                    _starTime = DateTime.Now;
                    return connection.QueryFirstOrDefault<TOther>(sql, thatParam);
                }
            }
            catch (Exception ex)
            {
                LogSql(sql, thatParam, ex);
                isError = true;
                throw;
            }
            finally
            {
                if (!isError) LogSql(sql, thatParam);
            }
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected IEnumerable<TOther> Query<TOther>(string sql, object param = null)
        {
            _explainSpan = DateTime.Now - _starTime;
            _starTime = DateTime.Now;
            var thatParam = param ?? _params;
            var isError = false;
            try
            {
                using (var connection = new MySqlConnection(GetTableInfo().ConnectionString))
                {
                    _connSpan = DateTime.Now - _starTime;
                    _starTime = DateTime.Now;
                    return connection.Query<TOther>(sql, thatParam);
                }
            }
            catch (Exception ex)
            {
                LogSql(sql, thatParam, ex);
                isError = true;
                throw;
            }
            finally
            {
                if (!isError) LogSql(sql, thatParam);
            }
        }

        /// <summary>
        /// 执行多个
        /// </summary>
        /// <typeparam name="TForeign"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected (T main, IEnumerable<TForeign> foreign) QueryMultiple<TForeign>(string sql, object param = null)
        {
            _explainSpan = DateTime.Now - _starTime;
            _starTime = DateTime.Now;
            var thatParam = param ?? _params;
            var isError = false;
            try
            {
                using (var connection = new MySqlConnection(GetTableInfo().ConnectionString))
                {
                    _connSpan = DateTime.Now - _starTime;
                    _starTime = DateTime.Now;
                    var read = connection.QueryMultiple(sql, thatParam);
                    var a = read.Read();
                    var b = read.Read(); // todo 暂时不能实现 搞不定
                    return (default(T), new List<TForeign>());
                }
            }
            catch (Exception ex)
            {
                LogSql(sql, thatParam, ex);
                isError = true;
                throw;
            }
            finally
            {
                if (!isError) LogSql(sql, thatParam);
            }
        }
    }
}
