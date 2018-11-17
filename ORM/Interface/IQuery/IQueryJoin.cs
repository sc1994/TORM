using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IQueryJoin<T, TFunc, TFuncBool> : IQueryWhere<T, TFunc, TFuncBool>
    {
        IQueryJoin<T, TFunc, TFuncBool> Join(params Expression<TFuncBool>[] exp);
        IQueryJoin<T, TFunc, TFuncBool> JoinL(params Expression<TFuncBool>[] exp);
        IQueryJoin<T, TFunc, TFuncBool> JoinR(params Expression<TFuncBool>[] exp);
        IQueryJoin<T, TFunc, TFuncBool> JoinF(params Expression<TFuncBool>[] exp);
    }
}
