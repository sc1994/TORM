using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IQueryOrder<T> : IQueryGroup<T>
    {
        IQueryOrder<T> OrderA(Expression<Func<T, object[]>> exp);
        IQueryOrder<T> OrderD(Expression<Func<T, object[]>> exp);
        IQueryOrder<T> OrderA(params Expression<Func<T, object>>[] exps);
        IQueryOrder<T> OrderD(params Expression<Func<T, object>>[] exps);
    }

    public interface IQueryOrder<T, TJoin> : IQueryGroup<T, TJoin>
    {
        IQueryOrder<T, TJoin> OrderA(Expression<Func<T, TJoin, object[]>> exp);
        IQueryOrder<T, TJoin> OrderD(Expression<Func<T, TJoin, object[]>> exp);
        IQueryOrder<T, TJoin> OrderA(params Expression<Func<T, TJoin, object>>[] exps);
        IQueryOrder<T, TJoin> OrderD(params Expression<Func<T, TJoin, object>>[] exps);
    }

    public interface IQueryOrder<T, TJoin1, TJoin2> : IQueryGroup<T, TJoin1, TJoin2>
    {
        IQueryOrder<T, TJoin1, TJoin2> OrderA(Expression<Func<T, TJoin1, TJoin2, object[]>> exp);
        IQueryOrder<T, TJoin1, TJoin2> OrderD(Expression<Func<T, TJoin1, TJoin2, object[]>> exp);
        IQueryOrder<T, TJoin1, TJoin2> OrderA(params Expression<Func<T, TJoin1, TJoin2, object>>[] exps);
        IQueryOrder<T, TJoin1, TJoin2> OrderD(params Expression<Func<T, TJoin1, TJoin2, object>>[] exps);
    }

    public interface IQueryOrder<T, TJoin1, TJoin2, TJoin3> : IQueryGroup<T, TJoin1, TJoin2, TJoin3>
    {
        IQueryOrder<T, TJoin1, TJoin2, TJoin3> OrderA(Expression<Func<T, TJoin1, TJoin2, TJoin3, object[]>> exp);
        IQueryOrder<T, TJoin1, TJoin2, TJoin3> OrderD(Expression<Func<T, TJoin1, TJoin2, TJoin3, object[]>> exp);
        IQueryOrder<T, TJoin1, TJoin2, TJoin3> OrderA(params Expression<Func<T, TJoin1, TJoin2, TJoin3, object>>[] exps);
        IQueryOrder<T, TJoin1, TJoin2, TJoin3> OrderD(params Expression<Func<T, TJoin1, TJoin2, TJoin3, object>>[] exps);
    }

    public interface IQueryOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> : IQueryGroup<T, TJoin1, TJoin2, TJoin3, TJoin4>
    {
        IQueryOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> OrderA(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object[]>> exp);
        IQueryOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> OrderD(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object[]>> exp);
        IQueryOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> OrderA(params Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>>[] exps);
        IQueryOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> OrderD(params Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>>[] exps);
    }
}
