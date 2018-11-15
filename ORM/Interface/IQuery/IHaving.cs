using System;
using System.Linq.Expressions;

namespace ORM.Interface.IQuery
{
    public interface IHaving<T> : IQuery<T>
    {
        IHaving<T> Having(Expression<Func<T, bool>> exp);
    }

    public interface IHaving<T, TJoin> : IQuery<T>
    {
        IHaving<T, TJoin> Having(Expression<Func<T, TJoin, bool>> exp);
    }

    public interface IHaving<T, TJoin1, TJoin2> : IQuery<T>
    {
        IHaving<T, TJoin1, TJoin2> Having(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
    }

    public interface IHaving<T, TJoin1, TJoin2, TJoin3> : IQuery<T>
    {
        IHaving<T, TJoin1, TJoin2, TJoin3> Having(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
    }

    public interface IHaving<T, TJoin1, TJoin2, TJoin3, TJoin4> : IQuery<T>
    {
        IHaving<T, TJoin1, TJoin2, TJoin3, TJoin4> Having(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
    }
}
