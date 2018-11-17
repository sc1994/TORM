namespace ORM.Interface
{
    public interface IQueryWhere<T, TFunc, TFuncBool> :
        IWhere,
        IWhere<TFuncBool, IQueryWhere<T, TFunc, TFuncBool>>,
        IQueryGroup<T, TFunc, TFuncBool>
    {

    }
}
