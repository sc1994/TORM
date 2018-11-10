using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IUpdate
    {
        int Update(int top = 0);
        List<UpdateRecord> Update(int top = 0, bool record = false);
    }

    public interface IUpdateSet<T> : IUpdateWhere<T>
    {
        ISet<T> Set<TValue>(Expression<Func<T, TValue>> exp, string value);
        ISet<T> Set<TValue>(params (Expression<Func<T, object>> exp, string value)[] exps);
    }

    public interface IUpdateWhere<T> : IUpdate
    {
        IWhere<T> Where(Expression<Func<T, bool>> exp);
    }

    public class UpdateRecord
    {
        public string Field { get; set; }

        public object Old { get; set; }

        public object New { get; set; }
    }
}
