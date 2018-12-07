namespace ORM.Interface
{
    /// <summary>
    /// WHERE
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFunc"></typeparam>
    /// <typeparam name="TFuncBool"></typeparam>
    public interface IQueryWhere<T, TFunc, TFuncBool> :
        IWhere,
        IWhere<TFuncBool, IQueryWhere<T, TFunc, TFuncBool>>,
        IQueryGroup<T, TFunc, TFuncBool>
    {

    }
}
