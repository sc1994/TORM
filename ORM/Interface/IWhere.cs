using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IWhere { }

    public interface IWhere<T, out TResult> where TResult : IWhere
    {
        TResult Where(Expression<Func<T, bool>> exp);
    }

    public interface IWhere<T, TJoin, out TResult> where TResult : IQueryWhere<T, TJoin>
    {
        TResult Where(Expression<Func<T, TJoin, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2, out TResult> where TResult : IQueryWhere<T, TJoin1, TJoin2>
    {
        TResult Where(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2, TJoin3, out TResult> where TResult : IQueryWhere<T, TJoin1, TJoin2, TJoin3>
    {
        TResult Where(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
    }

    public interface IWhere<T, TJoin1, TJoin2, TJoin3, TJoin4, out TResult> where TResult : IQueryWhere<T, TJoin1, TJoin2, TJoin3, TJoin4>
    {
        TResult Where(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
    }
}
