using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dapper;
using MySql.Data.MySqlClient;
using ORM.Interface;

namespace ORM.Realizes
{
    /// <summary>
    /// 解析 更新 相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeUpdate<T> : RealizeToSql<T>, IUpdateSet<T>
    {
        public int Update(Transaction transaction = null)
        {
            var sql = $"UPDATE {GetTableName()}{GetSet()}{GetWhere()}";
            MySqlConnection connection;
            if (transaction != null)
            {
                connection = _connections[transaction.Sole].connection;
                return connection.Execute(sql, _params, _connections[transaction.Sole].transaction);
            }
            else
            {
                connection = new MySqlConnection(); // todo 连接
                return connection.Execute(sql, _params);
            }
        }

        public int Update(int top, Transaction transaction = null)
        {
            var sql = ToTop(top);
            sql = string.Format(sql, $"{GetSet()}{GetWhere()}");
            throw new System.NotImplementedException();
        }

        public int Update(T model, Transaction transaction = null)
        {
            throw new System.NotImplementedException();
        }

        public IUpdateSet<T> Set<TValue>(params (Expression<Func<T, TValue>> exp, string value)[] exps)
        {
            foreach (var (exp, value) in exps)
            {
                _set.Add((exp, value));
            }
            return this;
        }

        public IUpdateWhere<T> Where(params Expression<Func<T, bool>>[] exps)
        {
            _where.AddRange(exps);
            return this;
        }
    }
}
