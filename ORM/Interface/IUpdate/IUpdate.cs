namespace ORM.Interface
{
    /// <summary>
    /// 定义 update
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUpdate<in T>
    {
        /// <summary>
        /// 执行更新
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        long Update(Transaction transaction = null);
        /// <summary>
        /// 执行更新
        /// </summary>
        /// <param name="top"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        long Update(int top, Transaction transaction = null);
        /// <summary>
        /// 更新 model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        long Update(T model, Transaction transaction = null);
    }
}
