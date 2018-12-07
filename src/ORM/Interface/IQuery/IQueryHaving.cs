using System.Linq.Expressions;

namespace ORM.Interface.IQuery
{
    /// <summary>
    /// HAVING
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFunc"></typeparam>
    /// <typeparam name="TFuncBool"></typeparam>
    public interface IQueryHaving<T, TFunc, TFuncBool> : IQueryOrder<T, TFunc>
    {
        /// <summary>
        /// HAVING
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQueryHaving<T, TFunc, TFuncBool> Having(params Expression<TFuncBool>[] exps);
    }
}
