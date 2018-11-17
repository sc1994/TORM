using System.Linq.Expressions;

namespace ORM.Interface.IQuery
{
    public interface IQueryHaving<T, TFunc, TFuncBool> : IQueryOrder<T, TFunc>
    {
        IQueryHaving<T, TFunc, TFuncBool> Having(params Expression<TFuncBool>[] exp);
    }
}
