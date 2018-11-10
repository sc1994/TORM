using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IWhere<T> : IOrder<T>
    {
        IWhere<T> Where(Expression<Func<T, bool>> exp);
    }

    public interface IWhere<T, TJoin> : IOrder<T, TJoin>, IUpdate
    {
        IWhere<T, TJoin> Where(Expression<Func<T, TJoin, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2> : IOrder<T, TJoin1, TJoin2>, IUpdate
    {
        IWhere<T, TJoin1, TJoin2> Where(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2, TJoin3> : IOrder<T, TJoin1, TJoin2, TJoin3>, IUpdate
    {
        IWhere<T, TJoin1, TJoin2, TJoin3> Where(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2, TJoin3, TJoin4> : IOrder<T, TJoin1, TJoin2, TJoin3, TJoin4>, IUpdate
    {
        IWhere<T, TJoin1, TJoin2, TJoin3, TJoin4> Where(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
    }
}
