using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IOrder<T> : IGroup<T>
    {
        IOrder<T> OrderA(Expression<Func<T, object[]>> exp);
        IOrder<T> OrderD(Expression<Func<T, object[]>> exp);
        IOrder<T> OrderA(params Expression<Func<T, object>>[] exps);
        IOrder<T> OrderD(params Expression<Func<T, object>>[] exps);
    }

    public interface IOrder<T, TJoin> : IGroup<T, TJoin>
    {
        IOrder<T, TJoin> OrderA(Expression<Func<T, TJoin, object[]>> exp);
        IOrder<T, TJoin> OrderD(Expression<Func<T, TJoin, object[]>> exp);
        IOrder<T, TJoin> OrderA(params Expression<Func<T, TJoin, object>>[] exps);
        IOrder<T, TJoin> OrderD(params Expression<Func<T, TJoin, object>>[] exps);
    }

    public interface IOrder<T, TJoin1, TJoin2> : IGroup<T, TJoin1, TJoin2>
    {
        IOrder<T, TJoin1, TJoin2> OrderA(Expression<Func<T, TJoin1, TJoin2, object[]>> exp);
        IOrder<T, TJoin1, TJoin2> OrderD(Expression<Func<T, TJoin1, TJoin2, object[]>> exp);
        IOrder<T, TJoin1, TJoin2> OrderA(params Expression<Func<T, TJoin1, TJoin2, object>>[] exps);
        IOrder<T, TJoin1, TJoin2> OrderD(params Expression<Func<T, TJoin1, TJoin2, object>>[] exps);
    }

    public interface IOrder<T, TJoin1, TJoin2, TJoin3> : IGroup<T, TJoin1, TJoin2, TJoin3>
    {
        IOrder<T, TJoin1, TJoin2, TJoin3> OrderA(Expression<Func<T, TJoin1, TJoin2, TJoin3, object[]>> exp);
        IOrder<T, TJoin1, TJoin2, TJoin3> OrderD(Expression<Func<T, TJoin1, TJoin2, TJoin3, object[]>> exp);
        IOrder<T, TJoin1, TJoin2, TJoin3> OrderA(params Expression<Func<T, TJoin1, TJoin2, TJoin3, object>>[] exps);
        IOrder<T, TJoin1, TJoin2, TJoin3> OrderD(params Expression<Func<T, TJoin1, TJoin2, TJoin3, object>>[] exps);
    }

    public interface IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> : IGroup<T, TJoin1, TJoin2, TJoin3, TJoin4>
    {
        IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> OrderA(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object[]>> exp);
        IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> OrderD(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object[]>> exp);
        IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> OrderA(params Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>>[] exps);
        IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> OrderD(params Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>>[] exps);
    }
}
