using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IQueryJoin<T, TJoin> : IQueryWhere<T, TJoin>
    {
        IQueryJoin<T, TJoin> Join(Expression<Func<T, TJoin, bool>> exp);
        IQueryJoin<T, TJoin> JoinL(Expression<Func<T, TJoin, bool>> exp);
        IQueryJoin<T, TJoin> JoinR(Expression<Func<T, TJoin, bool>> exp);
        IQueryJoin<T, TJoin> JoinF(Expression<Func<T, TJoin, bool>> exp);
    }

    public interface IQueryJoin<T, TJoin1, TJoin2> : IQueryWhere<T, TJoin1, TJoin2>
    {
        IQueryJoin<T, TJoin1, TJoin2> Join(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2> JoinL(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2> JoinR(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2> JoinF(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
    }

    public interface IQueryJoin<T, TJoin1, TJoin2, TJoin3> : IQueryWhere<T, TJoin1, TJoin2, TJoin3>
    {
        IQueryJoin<T, TJoin1, TJoin2, TJoin3> Join(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2, TJoin3> JoinL(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2, TJoin3> JoinR(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2, TJoin3> JoinF(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
    }

    public interface IQueryJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> : IQueryWhere<T, TJoin1, TJoin2, TJoin3, TJoin4>
    {
        IQueryJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> Join(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> JoinL(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> JoinR(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
        IQueryJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> JoinF(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
    }
}
