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
            return Execute(sql, transaction);
        }

        public int Update(int top, Transaction transaction = null)
        {
            var sql = ToTop(top);
            sql = string.Format(sql, $"{GetSet()}{GetWhere()}");
            return Execute(sql, transaction);
        }

        public int Update(T model, Transaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IUpdateSet<T> Set<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            _set.Add((exp, value));
            return this;
        }

        public IUpdateSet<T> Set<TValue>(params (Expression<Func<T, TValue>> exp, TValue value)[] exps)
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
