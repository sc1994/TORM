using ORM.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Realizes
{
    /// <summary>
    /// 实现 insert
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeInsert<T> : RealizeCommon<T>, IInsert<T>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long Insert(T model, Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var sql = GetInsert();
            return Execute(sql, transaction, model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="models"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long InsertBatch(IEnumerator<T> models, Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var sql = GetInsert();
            return Execute(sql, transaction, models);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="models"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long InsertBatch(T[] models, Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var sql = GetInsert();
            return Execute(sql, transaction, models);
        }

        /// <summary>
        /// 获取 insert sql
        /// </summary>
        /// <returns></returns>
        private string GetInsert()
        {
            var typeT = ChenkT();
            var key = $"GetInsert_{typeT.Name}";
            if (Stores.SqlDic.TryGetValue(key, out var sql))
            {
                return sql;
            }
            var properties = typeT.GetProperties();

            var sqlField = new StringBuilder();
            var sqlValue = new StringBuilder();
            foreach (var item in properties)
            {
                var fieldInfo = GetFieldInfo(item);
                if (!fieldInfo.Identity && string.IsNullOrWhiteSpace(fieldInfo.Foreign))
                {
                    sqlField.Append($"\r\n  `{item.Name}`,");
                    sqlValue.Append($"\r\n  @{item.Name},");
                }
            }

            sql = $"INSERT INTO {GetTableName()}\r\n({sqlField.SafeRemove(sqlField.Length - 1, 1)}\r\n)\r\nVALUES\r\n({sqlValue.SafeRemove(sqlValue.Length - 1, 1)}\r\n);";
            Stores.SqlDic.TryAdd(key, sql);
            return sql;
        }
    }
}
