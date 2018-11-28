using ORM.Interface.IDelete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ORM.Realizes
{
    /// <summary>
    /// 实现 delete 操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeDelete<T> : RealizeCommon<T>, IDeleteWhere<T>
    {
        /// <summary>
        /// delete 条件
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IDeleteWhere<T> Where(params Expression<Func<T, bool>>[] exps)
        {
            _where.AddRange(exps);
            return this;
        }

        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long Delete(Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var sql = $"DELETE FROM {GetTableName()}{GetWhere()};";
            return Execute(sql, transaction);
        }

        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="top"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long Delete(int top, Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var sql = string.Format(ToTop(top), $"{GetTableName()}{GetWhere()}");
            return Execute(sql, transaction);
        }

        /// <summary>
        /// 依据主键删除
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="key"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long Delete<TKey>(TKey key, Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var tableInfo = GetTableInfo();
            var keyInfo = tableInfo.Key ?? tableInfo.Identity;
            if (keyInfo == null) throw new Exception("未设置主键或者自增键");
            var sql = $"DELETE FROM {GetTableName()} WHERE {keyInfo.Name}=@key;";
            return Execute(sql, transaction, new { key });
        }

        /// <summary>
        /// top 值
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        private string ToTop(int top)
        {
            if (GetTableInfo().DBType == DBTypeEnum.MySQL)
            {
                return $"DELETE FROM {{0}}\r\nLIMIT {top};";
            }
            throw new NotImplementedException("为实现的top方式");
        }
    }
}
