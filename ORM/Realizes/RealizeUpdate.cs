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
        public int Update(Transaction transaction = null)
        {
            var sql = $"UPDATE {GetTableName()}{GetSet()}{GetWhere()};";
            return Execute(sql, transaction);
        }

        public int Update(int top, Transaction transaction = null)
        {
            //var sql = ToTop(top);
            //sql = string.Format(sql, $"{GetSet()}{GetWhere()}");
            //return Execute(sql, transaction);
            throw new NotImplementedException();
        }

        public int Update(T model, Transaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public IUpdateSet<T> Set<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            _set.Add((exp, value));
            return this;
        }

        public IUpdateSet<T> Set<TValue>(params (Expression<Func<T, TValue>> exp, TValue value)[] exps)
        {
            foreach (var (exp, value) in exps)
            {
                _set.Add((exp, value));
            }
            return this;
        }

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
            return GetSliceSql(SqlTypeEnum.Order,
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

                                   return result.TryRemove(result.Length - 1, 1);
                               });
        }
    }
}
