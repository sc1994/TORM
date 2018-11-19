using Dapper;
using MySql.Data.MySqlClient;
using ORM.Interface;
using System;
using System.Linq.Expressions;

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
            var sql = $"UPDATE {GetTableName()}{GetSet()}{GetWhere()};";
            MySqlConnection connection;
            if (transaction != null)
            {
                connection = Transaction.Connections[transaction.Sole].connection;
                connection.ConnectionString = GetTableInfo().ConnectionString;
                return connection.Execute(sql, _params, Transaction.Connections[transaction.Sole].transaction);
            }

            using (connection = new MySqlConnection(GetTableInfo().ConnectionString)) // todo 连接
            {
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
