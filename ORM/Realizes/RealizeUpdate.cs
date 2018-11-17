using System;
using System.Linq.Expressions;
using ORM.Interface;

namespace ORM.Realizes
{
    /// <summary>
    /// 解析 更新 相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeUpdate<T> : RealizeToSql<T>, IUpdateSet<T>
    {
        public int Update()
        {
            var sql = $"UPDATE {GetTableName()}{GetSet()}{GetWhere()}";
            throw new System.NotImplementedException();
        }

        public int Update(int top)
        {
            var sql = ToTop(top);
            sql = string.Format(sql, $"{GetSet()}{GetWhere()}");
            throw new System.NotImplementedException();
        }

        public int Update(T model)
        {
            throw new System.NotImplementedException();
        }

        public IUpdateSet<T> Set<TValue>(params (Expression<Func<T, TValue>> exp, string value)[] exps)
        {
            foreach (var item in exps)
            {
                _set.Add((item.exp, item.value));
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
