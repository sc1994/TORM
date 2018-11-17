using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IQuerySelect<T, TFunc, TFuncBool> : IQueryWhere<T, TFunc, TFuncBool>
    {
        IQuerySelect<T, TFunc, TFuncBool> Select(Expression<TFunc> exp);
        IQuerySelect<T, TFunc, TFuncBool> Select(params Expression<TFunc>[] exps);

        IQuerySelect<T, TFunc, TFuncBool> Select(Expression<TFunc> exp, string alias);
        IQuerySelect<T, TFunc, TFuncBool> Select(params (Expression<TFunc> exp, string alias)[] exps);
    }
}
