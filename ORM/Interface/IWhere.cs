using System.Linq.Expressions;

namespace ORM.Interface
{
    /// <summary>
    /// IWhere 基类
    /// </summary>
    public interface IWhere { }

    /// <summary>
    /// 多实现的 IWhere
    /// </summary>
    /// <typeparam name="TFunc"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IWhere<TFunc, out TResult> where TResult : IWhere
    {
        /// <summary>
        /// WHERE
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        TResult Where(params Expression<TFunc>[] exps);
    }
}
