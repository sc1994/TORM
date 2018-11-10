using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IJoin<T, TJoin> : IWhere<T, TJoin>
    {
        IJoin<T, TJoin> Join(Expression<Func<T, TJoin, bool>> exp);
        IJoin<T, TJoin> JoinL(Expression<Func<T, TJoin, bool>> exp);
        IJoin<T, TJoin> JoinR(Expression<Func<T, TJoin, bool>> exp);
        IJoin<T, TJoin> JoinF(Expression<Func<T, TJoin, bool>> exp);
    }

    public interface IJoin<T, TJoin1, TJoin2> : IWhere<T, TJoin1, TJoin2>
    {
        IJoin<T, TJoin1, TJoin2> Join(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
        IJoin<T, TJoin1, TJoin2> JoinL(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
        IJoin<T, TJoin1, TJoin2> JoinR(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
        IJoin<T, TJoin1, TJoin2> JoinF(Expression<Func<T, TJoin1, TJoin2, bool>> exp);
    }

    public interface IJoin<T, TJoin1, TJoin2, TJoin3> : IWhere<T, TJoin1, TJoin2, TJoin3>
    {
        IJoin<T, TJoin1, TJoin2, TJoin3> Join(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
        IJoin<T, TJoin1, TJoin2, TJoin3> JoinL(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
        IJoin<T, TJoin1, TJoin2, TJoin3> JoinR(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
        IJoin<T, TJoin1, TJoin2, TJoin3> JoinF(Expression<Func<T, TJoin1, TJoin2, TJoin3, bool>> exp);
    }

    public interface IJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> : IWhere<T, TJoin1, TJoin2, TJoin3, TJoin4>
    {
        IJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> Join(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
        IJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> JoinL(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
        IJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> JoinR(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
        IJoin<T, TJoin1, TJoin2, TJoin3, TJoin4> JoinF(Expression<Func<T, TJoin1, TJoin2, TJoin3, TJoin4, bool>> exp);
    }
}
