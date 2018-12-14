using Explain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ORM.Realizes
{
    /// <summary>
    /// to sql
    /// </summary>
    // ReSharper disable once UnusedTypeParameter
    public partial class RealizeCommon<T>
    {
        /// <summary>
        /// 转到 select
        /// </summary>
        /// <param name="item"></param>
        /// <param name="alias"></param>
        /// <param name="result"></param>
        protected void ToSelect(Expression item, string alias, StringBuilder result)
        {
            var c = new ContentEasy();
            ExplainTool.Explain(item, c);
            c.Rinse();
            c.Info.ForEach(x =>
            {
                var a = string.IsNullOrWhiteSpace(alias) ? "" : $" AS {alias}";
                result.Append(string.IsNullOrWhiteSpace(x.Method)
                                  ? $"\r\n  {GetTableName(x.Table)}.{x.Field}{a},"
                                  : $"\r\n  {x.Method.ToUpper()}({GetTableName(x.Table)}.{x.Field}){a},");
            });
        }

        /// <summary>
        /// 获取where sql 代码
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetWhere()
        {
            return GetSliceSql(SqlTypeEnum.Where, () =>
            {
                var result = new StringBuilder("\r\nWHERE\r\n  1=1");
                ToWhere(_where, result);
                return result;
            });
        }

        /// <summary>
        /// 转到 条件筛选
        /// </summary>
        /// <param name="exps"></param>
        /// <param name="result"></param>
        protected void ToWhere(List<Expression> exps, StringBuilder result)
        {
            foreach (var item in exps)
            {
                var c = new ContentWhere();
                ExplainTool.Explain(item, c);
                c.Rinse();
                result.Append("\r\nAND(");
                c.Info.ForEach(x =>
                {
                    string param;
                    var type = x.Type.ToExplain();
                    if (x.Value == null && (type == "=" || type == "<>"))
                    {
                        param = "null";
                        type = type == "=" ? "IS" : "IS NOT";
                    }
                    else
                    {
                        param = $"@{GetTableName(x.Table)}_{x.Field}_{_params.Count}";
                        if (x.Value is MemberInfo member)
                        {
                            _params.Add(param, SwitchTime(member));
                        }
                        else
                        {
                            _params.Add(param, x.Value);
                        }
                    }

                    var ex = x.Prior.ToExplain();
                    var sql = $"\r\n  {(ex == null ? "" : ex + " ")}{GetTableName(x.Table)}.{x.Field} ";
                    if (ExplainTool.Methods.Contains(x.Method))
                    {
                        if (x.Method == "Contains")
                        {
                            sql += $"LIKE '%'+{param}+'%'";
                        }
                        else if (x.Method == "StartsWith")
                        {
                            sql += $"LIKE {param}+'%'";
                        }
                        else if (x.Method == "EndsWith")
                        {
                            sql += $"LIKE '%'+{param}";
                        }
                        else if (x.Method == "In")
                        {
                            sql += $"IN ({param})";
                        }
                        else if (x.Method == "NotIn")
                        {
                            sql += $"NOT IN ({param})";
                        }
                    }
                    else
                    {
                        sql += $"{type} {param}";
                    }
                    result.Append(sql);
                });
                result.Append("\r\n)");
            }
        }

        /// <summary>
        /// 获取 group sql 代码
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetGroup()
        {
            return GetSliceSql(SqlTypeEnum.Group, () =>
            {
                var result = new StringBuilder();
                _groups.ForEach(item =>
                {
                    var c = new ContentEasy();
                    ExplainTool.Explain(item, c);
                    c.Rinse();
                    foreach (var info in c.Info)
                    {
                        result.Append($"\r\n  {GetTableName(info.Table)}.{info.Field},");
                    }
                });
                if (result.Length > 0)
                {
                    result.Insert(0, "\r\nGROUP BY");
                }
                return result.SafeRemove(result.Length - 1, 1);
            });
        }

        /// <summary>
        /// 获取 order sql 代码
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetOrder()
        {
            return GetSliceSql(SqlTypeEnum.Order, () =>
            {
                var result = new StringBuilder();
                _orders.ForEach(item =>
                {
                    var c = new ContentEasy();
                    ExplainTool.Explain(item.Item1, c);
                    c.Rinse();
                    foreach (var info in c.Info)
                    {
                        result.Append($"\r\n  {GetTableName(info.Table)}.{info.Field} {item.Item2.ToExplain()},");
                    }
                });
                if (result.Length > 0)
                {
                    result.Insert(0, "\r\nORDER BY");
                }
                return result.SafeRemove(result.Length - 1, 1);
            });
        }

        /// <summary>
        /// 获取 having sql 代码
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetHaving()
        {
            return GetSliceSql(SqlTypeEnum.Having, () =>
            {
                var result = new StringBuilder("\r\nHAVING 1=1");
                ToWhere(_having, result);
                if (result.ToString() == "\r\nHAVING 1=1")
                    result.Clear();
                return result;
            });
        }

        /// <summary>
        /// 获取 select *
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected StringBuilder GetAllSelect(Type type)
        {
            var table = GetTableInfo(type);
            var properties = type.GetProperties();
            var result = new StringBuilder();
            foreach (var item in properties)
            {
                var field = GetFieldInfo(item);
                result.Append($"  {table.Name}.{field.Name},");
            }
            return result.SafeRemove(0, 1);
        }

        /// <summary>
        /// 将sql分块，前后加上字典，以优化性能。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected StringBuilder GetSliceSql(SqlTypeEnum type, Func<StringBuilder> func)
        {
            // 字段取值
            if (_sqlDic.ContainsKey(type))
            {
                return _sqlDic[type];
            }
            // 字典中没有相应的值，执行委托
            var result = func();
            // 存入字典，以备下一次调用
            _sqlDic.Add(type, result);
            return result;
        }


    }
}
