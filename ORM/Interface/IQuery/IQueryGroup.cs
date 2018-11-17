using ORM.Interface.IQuery;

namespace ORM.Interface
{
    public interface IQueryGroup<T, TFunc, TFuncBool> : IQueryHaving<T, TFunc, TFuncBool>
    {
        IQueryGroup<T, TFunc, TFuncBool> Group(TFunc exp);
        IQueryGroup<T, TFunc, TFuncBool> Group(params TFunc[] exps);
    }
}
