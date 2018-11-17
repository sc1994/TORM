using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IQuerySelect<T> : IQueryWhere<T, Func<T, object>, Func<T, bool>>
    {
        IQuerySelect<T> Select(params Expression<Func<T, object>>[] exps);

        IQuerySelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps);
    }

    public interface IQuerySelect<T, TFunc, TFuncBool> : IQueryJoin<T, TFunc, TFuncBool>
    {
        IQuerySelect<T, TFunc, TFuncBool> Select(params Expression<TFunc>[] exps);

        IQuerySelect<T, TFunc, TFuncBool> Select(params (Expression<TFunc> exp, string alias)[] exps);
    }
}
