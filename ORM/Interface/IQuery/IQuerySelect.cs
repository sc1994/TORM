using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IQuerySelect<T> : IQueryWhere<T>
    {
        IQuerySelect<T> Select(params Expression<Func<T, object>>[] exps);

        IQuerySelect<T> Select(Expression<Func<T, object>> exp, string alias);
        IQuerySelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps);

        IQuerySelect<T> Select(Expression<Func<T, object[]>> exp);
    }

    public interface IQuerySelect<T, TJoin> : IQueryJoin<T, TJoin>
    {
        IQuerySelect<T, TJoin> Select(params Expression<Func<T, TJoin, object>>[] exps);

        IQuerySelect<T, TJoin> Select(Expression<Func<T, TJoin, object>> exp, string alias);
        IQuerySelect<T, TJoin> Select(params (Expression<Func<T, TJoin, object>> exp, string alias)[] exps);

        IQuerySelect<T, TJoin> Select(Expression<Func<T, TJoin, object[]>> exp);
    }

    public interface IQuerySelect<T, TJoin1, TJoin2> : IQueryJoin<T, TJoin1, TJoin2>
    {
        IQuerySelect<T, TJoin1, TJoin2> Select(params Expression<Func<T, TJoin1, TJoin2, object>>[] exps);

        IQuerySelect<T, TJoin1, TJoin2> Select(Expression<Func<T, TJoin1, TJoin2, object>> exp, string alias);
        IQuerySelect<T, TJoin1, TJoin2> Select(params (Expression<Func<T, TJoin1, TJoin2, object>> exp, string alias)[] exps);

        IQuerySelect<T, TJoin1, TJoin2> Select(Expression<Func<T, TJoin1, TJoin2, object[]>> exp);
    }

    public interface IQuerySelect<T, TJoin1, TJoin2, TJoin3> : IQueryJoin<T, TJoin1, TJoin2, TJoin3>
    {
        IQuerySelect<T, TJoin1, TJoin2, TJoin3> Select(params Expression<Func<T, TJoin1, TJoin2, TJoin3, object>>[] exps);

        IQuerySelect<T, TJoin1, TJoin2, TJoin3> Select(Expression<Func<T, TJoin1, TJoin2, TJoin3, object>> exp, string alias);
        IQuerySelect<T, TJoin1, TJoin2, TJoin3> Select(params (Expression<Func<T, TJoin1, TJoin2, TJoin3, object>> exp, string alias)[] exps);

        IQuerySelect<T, TJoin1, TJoin2, TJoin3> Select(Expression<Func<T, TJoin1, TJoin2, TJoin3, object[]>> exp);
    }

    public interface IQuerySelect<T, TJoin1, TJoin2, TJoin3, TJoin4> : IQueryJoin<T, TJoin1, TJoin2, TJoin3, TJoin4>
    {
        IQuerySelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Select(params Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>>[] exps);

        IQuerySelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Select(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>> exp, string alias);
        IQuerySelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Select(params (Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>> exp, string alias)[] exps);

        IQuerySelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Select(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object[]>> exp);
    }
}
