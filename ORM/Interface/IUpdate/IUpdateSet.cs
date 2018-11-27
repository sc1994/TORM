using System;
using System.Linq.Expressions;

namespace ORM.Interface
{
    /// <summary>
    /// 定义 update set
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUpdateSet<T> : IUpdateWhere<T>
    {
        /// <summary>
        /// update set 设置
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="exp"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IUpdateSet<T> Set<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        /// <summary>
        /// update set 设置
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="exps"></param>
        /// <returns></returns>
        IUpdateSet<T> Set<TValue>(params (Expression<Func<T, TValue>> exp, TValue value)[] exps);
    }
}