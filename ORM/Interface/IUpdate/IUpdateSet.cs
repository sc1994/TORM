using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IUpdateSet<T> : IUpdateWhere<T>
    {
        IUpdateSet<T> Set<TValue>(params (Expression<Func<T, TValue>> exp, string value)[] exps);
    }
}