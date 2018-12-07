using System.Linq.Expressions;

namespace ORM.Interface
{
    /// <summary>
    /// JOIN
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFunc"></typeparam>
    /// <typeparam name="TFuncBool"></typeparam>
    public interface IQueryJoin<T, TFunc, TFuncBool> : IQueryWhere<T, TFunc, TFuncBool>
    {
        /// <summary>
        /// JOIN
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQueryJoin<T, TFunc, TFuncBool> Join(params Expression<TFuncBool>[] exps);
        /// <summary>
        /// JOIN LEFT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQueryJoin<T, TFunc, TFuncBool> JoinL(params Expression<TFuncBool>[] exps);
        /// <summary>
        /// JOIN RIGHT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQueryJoin<T, TFunc, TFuncBool> JoinR(params Expression<TFuncBool>[] exps);
        /// <summary>
        /// JOIN FULL
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQueryJoin<T, TFunc, TFuncBool> JoinF(params Expression<TFuncBool>[] exps);
    }
}
