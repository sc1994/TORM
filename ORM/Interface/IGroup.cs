using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IGroup<T> : IQuery<T>
    {
        IOrder<T> Group(Expression<Func<T, object[]>> exp);
        IOrder<T> Group(params Expression<Func<T, object>>[] exps);
    }

    public interface IGroup<T, TJoin> : IQuery<T>
    {
        IOrder<T, TJoin> Group(Expression<Func<T, TJoin, object[]>> exp);
        IOrder<T, TJoin> Group(params Expression<Func<T, TJoin, object>>[] exps);
    }

    public interface IGroup<T, TJoin1, TJoin2> : IQuery<T>
    {
        IOrder<T, TJoin1, TJoin2> Group(Expression<Func<T, TJoin1, TJoin2, object[]>> exp);
        IOrder<T, TJoin1, TJoin2> Group(params Expression<Func<T, TJoin1, TJoin2, object>>[] exps);
    }

    public interface IGroup<T, TJoin1, TJoin2, TJoin3> : IQuery<T>
    {
        IOrder<T, TJoin1, TJoin2, TJoin3> Group(Expression<Func<T, TJoin1, TJoin2, TJoin3, object[]>> exp);
        IOrder<T, TJoin1, TJoin2, TJoin3> Group(params Expression<Func<T, TJoin1, TJoin2, TJoin3, object>>[] exps);
    }

    public interface IGroup<T, TJoin1, TJoin2, TJoin3, TJoin4> : IQuery<T>
    {
        IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> Group(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object[]>> exp);
        IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4> Group(params Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, object>>[] exps);
    }
}
