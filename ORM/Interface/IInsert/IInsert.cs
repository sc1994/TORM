using System.Collections.Generic;

namespace ORM.Interface
{
    /// <summary>
    /// 定义IInsert
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IInsert<in T>
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        long Insert(T model, Transaction transaction = null);
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        long InsertBatch(IEnumerator<T> models, Transaction transaction = null);
    }
}
