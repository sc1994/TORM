using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IWhere<T> : IOrder<T>
    {
        IWhere<T> And(Expression<Func<T, bool>> exp);
        IWhere<T> Or(Expression<Func<T, bool>> exp);
    }

    public interface IWhere<T, TJoin> : IOrder<T, TJoin>, IUpdate
    {
        IWhere<T, TJoin> And(Expression<Func<T, TJoin, bool>> exp);
        IWhere<T, TJoin> Or(Expression<Func<T, TJoin, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2> : IOrder<T, TJoin1, TJoin2>, IUpdate
    {
        IWhere<T, TJoin1, TJoin2> And(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
        IWhere<T, TJoin1, TJoin2> Or(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2, TJoin3> : IOrder<T, TJoin1, TJoin2, TJoin3>, IUpdate
    {
        IWhere<T, TJoin1, TJoin2, TJoin3> And(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
        IWhere<T, TJoin1, TJoin2, TJoin3> Or(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2, TJoin3, TJoin4> : IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4>, IUpdate
    {
        IWhere<T, TJoin1, TJoin2, TJoin3, TJoin4> And(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
        IWhere<T, TJoin1, TJoin2, TJoin3, TJoin4> Or(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
    }
}
