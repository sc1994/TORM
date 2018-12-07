using System.Collections.Generic;

namespace ORM.Interface
{
    /// <summary>
    /// 定义Query
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQuery<T>
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        bool Exist();
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        long Count();
        /// <summary>
        /// Count Sql
        /// </summary>
        /// <returns></returns>
        string CountSql();
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <returns></returns>
        T First();
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <typeparam name="TOther">自定义返回数据格式</typeparam>
        /// <returns></returns>
        TOther First<TOther>();
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <returns></returns>
        IEnumerable<TOther> Find<TOther>();
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Find();
        /// <summary>
        /// 查询sql
        /// </summary>
        /// <returns></returns>
        string FindSql();
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="TOther">自定义返回数据格式</typeparam>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<TOther> Limit<TOther>(long count);
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<T> Limit(long count);
        /// <summary>
        /// 查询sql
        /// </summary>
        /// <param name="count">top值</param>
        /// <returns></returns>
        string FindSql(long count);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        (IEnumerable<T> data, long total) Page(long index, long size);
        /// <summary>
        /// 分页sql
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        string PageSql(long index, long size);
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TOther">自定义返回数据格式</typeparam>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        (IEnumerable<TOther> data, long total) Page<TOther>(long index, long size);
    }
}
