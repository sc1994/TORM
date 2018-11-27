using System.Linq.Expressions;

namespace ORM.Interface
{
    /// <summary>
    /// ORDER
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFunc"></typeparam>
    public interface IQueryOrder<T, TFunc> : IQuery<T>
    {
        /// <summary>
        /// ORDER BY  ASC
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQueryOrder<T, TFunc> OrderA(params Expression<TFunc>[] exps);

        /// <summary>
        /// ORDER BY DESC
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        IQueryOrder<T, TFunc> OrderD(params Expression<TFunc>[] exps);
    }
}
