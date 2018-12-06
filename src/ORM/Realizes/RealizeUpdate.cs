using Explain;
using ORM.Interface;
using System;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Realizes
{
    /// <summary>
    /// 解析 更新 相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeUpdate<T> : RealizeCommon<T>, IUpdateSet<T>
    {
        /// <summary>
        /// 执行更新
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long Update(Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var sql = $"UPDATE {GetTableName()}{GetSet()}{GetWhere()};";
            return Execute(sql, transaction);
        }

        /// <summary>
        /// 执行更新
        /// </summary>
        /// <param name="top"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long Update(int top, Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var sql = string.Format(ToTop(top), $"{GetTableName()}{GetSet()}{GetWhere()}");
            return Execute(sql, transaction);
        }

        /// <summary>
        /// 更新model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public long Update(T model, Transaction transaction = null)
        {
            _starTime = DateTime.Now;
            var sql = GetUpdateByModel(model);
            return Execute(sql, transaction, model);
        }

        /// <summary>
        /// update set 设置
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="exp"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IUpdateSet<T> Set<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            _set.Add((exp, value));
            return this;
        }

        /// <summary>
        /// update set 设置
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IUpdateSet<T> Set<TValue>(params (Expression<Func<T, TValue>> exp, TValue value)[] exps)
        {
            foreach (var (exp, value) in exps)
            {
                _set.Add((exp, value));
            }
            return this;
        }

        /// <summary>
        /// update where 条件
        /// </summary>
        /// <param name="exps"></param>
        /// <returns></returns>
        public IUpdateWhere<T> Where(params Expression<Func<T, bool>>[] exps)
        {
            _where.AddRange(exps);
            return this;
        }

        /// <summary>
        /// 获取 set sql 代码
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetSet()
        {
            return GetSliceSql(SqlTypeEnum.Set,
                               () =>
                               {
                                   var result = new StringBuilder("\r\nSET");
                                   foreach (var item in _set)
                                   {
                                       var c = new ContentEasy();
                                       ExplainTool.Explain(item.Item1, c);
                                       if (c.Info.Count < 1)
                                       {
                                           throw new Exception("Set表达式内容为空");
                                       }
                                       var x = c.Info[0];
                                       var param = $"@{GetTableName(x.Table)}_{x.Field}_{_params.Count}";
                                       _params.Add(param, item.Item2);
                                       result.Append($"\r\n  {GetTableName(x.Table)}.{x.Field} = {param},");
                                   }

                                   return result.SafeRemove(result.Length - 1, 1);
                               });
        }

        private string ToTop(int top)
        {
            if (GetTableInfo().DBType == DBTypeEnum.MySQL)
            {
                return $"UPDATE {{0}}\r\nLIMIT {top};";
            }
            throw new NotImplementedException("为实现的top方式");
        }

        /// <summary>
        /// 获取 insert sql
        /// </summary>
        /// <returns></returns>
        private string GetUpdateByModel(T model)
        {
            var typeT = ChenkT();
            var key = $"GetUpdateByModel_{typeT.Name}";
            if (Stores.SqlDic.TryGetValue(key, out var sql))
            {
                return sql;
            }
            var properties = typeT.GetProperties();
            var sqlField = new StringBuilder();
            var fieldKey = string.Empty;
            foreach (var item in properties)
            {
                var fieldInfo = GetFieldInfo(item);
                if (!fieldInfo.Identity && !fieldInfo.Key)
                {
                    sqlField.Append($"\r\n  {item.Name} = @{item.Name},");
                }
                else if (fieldInfo.Key && fieldInfo.Identity)
                {
                    fieldKey = fieldInfo.Name;
                }
                _params.Add(fieldInfo.Name, item.GetValue(model));
            }

            sql = $"UPDATE {GetTableName()} SET{sqlField.SafeRemove(sqlField.Length - 1, 1)}\r\nWHERE\r\n  {fieldKey} = @{fieldKey};";
            Stores.SqlDic.TryAdd(key, sql);
            return sql;
        }
    }
}
