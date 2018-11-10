using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface ISelect<T> : IWhere<T>
    {
        ISelect<T> Select(params Expression<Func<T, object>>[] exps);

        ISelect<T> Select(Expression<Func<T, object>> exp, string alias);
        ISelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps);

        ISelect<T> Select(Expression<Func<T, object[]>> exp);
    }

    public interface ISelect<T, TJoin> : IJoin<T, TJoin>
    {
        ISelect<T, TJoin> Select(params Expression<Func<T, TJoin, object>>[] exps);

        ISelect<T, TJoin> Select(Expression<Func<T, TJoin, object>> exp, string alias);
        ISelect<T, TJoin> Select(params (Expression<Func<T, TJoin, object>> exp, string alias)[] exps);

        ISelect<T, TJoin> Select(Expression<Func<T, TJoin, object[]>> exp);
    }

    public interface ISelect<T, TJoin1, TJoin2> : IJoin<T, TJoin1, TJoin2>
    {
        ISelect<T, TJoin1, TJoin2> Select(params Expression<Func<T, TJoin1, TJoin2, object>>[] exps);

        ISelect<T, TJoin1, TJoin2> Select(Expression<Func<T, TJoin1, TJoin2, object>> exp, string alias);
        ISelect<T, TJoin1, TJoin2> Select(params (Expression<Func<T, TJoin1, TJoin2, object>> exp, string alias)[] exps);

        ISelect<T, TJoin1, TJoin2> Select(Expression<Func<T, TJoin1, TJoin2, object[]>> exp);
    }

    public interface ISelect<T, TJoin1, TJoin2, TJoin3> : IJoin<T, TJoin1, TJoin2, TJoin3>
    {
        ISelect<T, TJoin1, TJoin2, TJoin3> Select(params Expression<Func<T, TJoin1, TJoin2, TJoin3, object>>[] exps);

        ISelect<T, TJoin1, TJoin2, TJoin3> Select(Expression<Func<T, TJoin1, TJoin2, TJoin3, object>> exp, string alias);
        ISelect<T, TJoin1, TJoin2, TJoin3> Select(params (Expression<Func<T, TJoin1, TJoin2, TJoin3, object>> exp, string alias)[] exps);

        ISelect<T, TJoin1, TJoin2, TJoin3> Select(Expression<Func<T, TJoin1, TJoin2, TJoin3, object[]>> exp);
    }

    public interface ISelect<T, TJoin1, TJoin2, TJoin3, TJoin4> : IJoin<T, TJoin1, TJoin2, TJoin3, TJoin4>
    {
        ISelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Select(params Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>>[] exps);

        ISelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Select(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>> exp, string alias);
        ISelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Select(params (Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>> exp, string alias)[] exps);

        ISelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Select(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object[]>> exp);
    }
}
