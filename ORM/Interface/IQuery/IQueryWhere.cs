namespace ORM.Interface
{
    //public interface IQuerySelect<T> : IQueryWhere<T, Func<T, object>, Func<T, bool>>
    //{
    //    IQuerySelect<T> Select(params Expression<Func<T, object>>[] exps);

    //    IQuerySelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps);
    //}

    //public interface IQueryWhere<T, TFunc, TFuncBool> :
    //    IWhere,
    //    IWhere<TFuncBool, IQueryWhere<T, TFunc, TFuncBool>>,
    //    IQueryGroup<T, TFunc, TFuncBool>
    //{

    //}

    /// <summary>
    /// 实现
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
