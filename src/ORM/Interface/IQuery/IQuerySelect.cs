using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    /// <summary>
    /// 单表 select
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuerySelect<T> : IQueryWhere<T, Func<T, object>, Func<T, bool>>
    {
        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQuerySelect<T> Select(params Expression<Func<T, object>>[] exps);

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQuerySelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps);
    }

    /// <summary>
    /// 多表 select
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFunc"></typeparam>
    /// <typeparam name="TFuncBool"></typeparam>
    public interface IQuerySelect<T, TFunc, TFuncBool> : IQueryJoin<T, TFunc, TFuncBool>
    {
        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQuerySelect<T, TFunc, TFuncBool> Select(params Expression<TFunc>[] exps);

        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQuerySelect<T, TFunc, TFuncBool> Select(params (Expression<TFunc> exp, string alias)[] exps);
    }
}
