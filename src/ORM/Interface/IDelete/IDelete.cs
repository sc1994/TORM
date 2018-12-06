namespace ORM.Interface.IDelete
{
    /// <summary>
    /// 定义IDelete
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDelete<in T>
    {
        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        long Delete(Transaction transaction = null);

        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="top"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        long Delete(int top, Transaction transaction = null);

        /// <summary>
        /// 依据主键删除
        /// </summary>
        /// <typeparam name="TKey">主键</typeparam>
        /// <param name="key">值</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        long Delete<TKey>(TKey key, Transaction transaction = null);
    }
}
