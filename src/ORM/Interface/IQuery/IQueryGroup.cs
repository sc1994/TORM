using System.Linq.Expressions;
using ORM.Interface.IQuery;

namespace ORM.Interface
{
    /// <summary>
    /// GROUP
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFunc"></typeparam>
    /// <typeparam name="TFuncBool"></typeparam>
    public interface IQueryGroup<T, TFunc, TFuncBool> : IQueryHaving<T, TFunc, TFuncBool>
    {
        /// <summary>
        /// GROUP BY
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQueryGroup<T, TFunc, TFuncBool> Group(params Expression<TFunc>[] exps);
    }
}
