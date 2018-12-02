﻿using Explain;
using ORM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Realizes
{
    /// <summary>
    /// 解析 查询 相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeQuery<T> : RealizeCommon<T>, IQuery<T>
    {
        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <returns>是否存在</returns>
        public bool Exist()
        {
            _starTime = DateTime.Now;
            return Count() > 0; // todo 也许有不需要COUNT的高效办法
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns>count</returns>
        public long Count()
        {
            _starTime = DateTime.Now;
            return QueryFirst<long>(CountSql());
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns>sql</returns>
        public string CountSql()
        {
            _starTime = DateTime.Now;
            return $"SELECT COUNT(1) FROM {GetTableName()}{GetJoin()}{GetWhere()}{GetGroup()}{GetHaving()};";
        }

        /// <summary>
        /// 查找第一条数据
        /// </summary>
        /// <returns>T</returns>
        public T First()
        {
            _starTime = DateTime.Now;
            return QueryFirst<T>(FindSql());
        }

        /// <summary>
        /// 查找第一条数据
        /// </summary>
        /// <typeparam name="TOther">重新定义返回数据的格式</typeparam>
        /// <returns>TOther</returns>
        public TOther First<TOther>()
        {
            _starTime = DateTime.Now;
            return QueryFirst<TOther>(FindSql());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public string FindSql()
        {
            _starTime = DateTime.Now;
            return $"{GetSelect()}\r\nFROM {GetTableName()}{GetJoin()}{GetWhere()}{GetGroup()}{GetHaving()}{GetOrder()};";
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Find()
        {
            _starTime = DateTime.Now;
            return Query<T>(FindSql());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="TOther">重新定义返回数据的格式</typeparam>
        /// <returns></returns>
        public IEnumerable<TOther> Find<TOther>()
        {
            _starTime = DateTime.Now;
            return Query<TOther>(FindSql());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="top">限制获取数量</param>
        /// <returns></returns>
        public IEnumerable<T> Find(int top)
        {
            _starTime = DateTime.Now;
            return Query<T>(FindSql(top));
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="top">限制获取数量</param>
        /// <returns></returns>
        public string FindSql(int top)
        {
            _starTime = DateTime.Now;
            var t = ToTop(top);
            return string.Format(t, $"\r\nFROM {GetTableName()}{GetJoin()}{GetWhere()}{GetGroup()}{GetHaving()}{GetOrder()}");
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <typeparam name="TOther">重新定义返回数据的格式</typeparam>
        /// <param name="top">限制获取数量</param>
        /// <returns></returns>
        public IEnumerable<TOther> Find<TOther>(int top)
        {
            _starTime = DateTime.Now;
            var t = ToTop(top);
            var sql = string.Format(t, $"\r\nFROM {GetTableName()}{GetJoin()}{GetWhere()}{GetGroup()}{GetHaving()}{GetOrder()}");
            return Query<TOther>(sql);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public string PageSql(int index, int size)
        {
            _starTime = DateTime.Now;
            var t = ToPage(index, size);
            return string.Format(t, $"\r\nFROM {GetTableName()}{GetJoin()}{GetWhere()}{GetGroup()}{GetHaving()}{GetOrder()}");
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="index">当前页</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public (IEnumerable<T> data, long total) Page(int index, int size)
        {
            _starTime = DateTime.Now;
            return (Query<T>(PageSql(index, size)), Count());
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TOther">重新定义返回数据的格式</typeparam>
        /// <param name="index">当前页</param>
        /// <param name="size">页大小</param>
        /// <returns></returns>
        public (IEnumerable<TOther> data, long total) Page<TOther>(int index, int size)
        {
            _starTime = DateTime.Now;
            return (Query<TOther>(PageSql(index, size)), Count());
        }

        /// <summary>
        /// 获取 select sql 代码
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetSelect()
        {
            return GetSliceSql(SqlTypeEnum.Select, () =>
            {
                var result = new StringBuilder("SELECT");
                _selects.ForEach(x => ToSelect(x, null, result));
                _selectAlias.ForEach(x => ToSelect(x.Item1, x.Item2, result));
                if (result.ToString() != "SELECT")
                {
                    result.SafeRemove(result.Length - 1, 1);
                }
                else
                {
                    result.Append("\r\n  *");
                }
                return result;
            });
        }

        /// <summary>
        /// 转到 select
        /// </summary>
        /// <param name="item"></param>
        /// <param name="alias"></param>
        /// <param name="result"></param>
        private void ToSelect(Expression item, string alias, StringBuilder result)
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
        /// 获取 group sql 代码
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetGroup()
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
        private StringBuilder GetOrder()
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
        /// 转成分页的
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        private string ToPage(int index, int size)
        {
            var t = GetTableInfo().DBType;
            var order = GetOrder();
            var select = GetSelect();
            if (string.IsNullOrWhiteSpace(order.ToString()))
            {
                throw new Exception("分页需传入排序依据");
            }

            if (t == DBTypeEnum.SQLServer2008)
            {
                return "SELECT * FROM\r\n" +
                       "(SELECT ROW_NUMBER() OVER(ORDER BY" +
                       $"{order.Replace("ORDER BY\r\n", "")}\r\n) AS ROWNUMBER," +
                       $"{select.Replace("SELECT\r\n", "\r\n")}{{0}}" +
                       $"\r\n) A WHERE ROWNUMBER BETWEEN {(index - 1) * size + 1} AND {index * size};";
            }

            if (t == DBTypeEnum.MySQL)
            {
                return $"{select}{{0}}\r\nLIMIT {(index - 1) * size + 1}, {index * size};";
            }
            else
            {
                throw new NotImplementedException("未实现的数据库类型");
            }
        }

        /// <summary>
        /// 获取 join sql 代码
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetJoin() // todo 对于 mysql 来说 full join 有点麻烦
        {
            return GetSliceSql(SqlTypeEnum.Join, () =>
            {
                var result = new StringBuilder();
                _join.ForEach(x =>
                {
                    var c = new ContentJoin();
                    ExplainTool.Explain(x.Item1, c);
                    c.Rinse();
                    // 收集全部表
                    allTables.AddRange(c.Info.Select(s => s.Table));
                    allTables.AddRange(c.Info.Select(s => s.Table2));

                    foreach (var info in c.Info)
                    {
                        string param;
                        var type = info.Type.ToExplain();
                        if (info.Value == null
                            && (type == "=" || type == "<>")
                            && info.Table2 == null
                            && string.IsNullOrWhiteSpace(info.Field2)) // 当不是和表字段的比较，且 == null 或者 != null时，采取SQL 语法
                        {
                            param = "null";
                            type = type == "=" ? "IS" : "IS NOT";
                        }
                        else
                        {
                            param = $"@{GetTableName(info.Table)}_{info.Field}_{_params.Count}";
                            _params.Add(param, info.Value);
                        }

                        if (info.Table2 != null && !string.IsNullOrWhiteSpace(info.Field2))
                        {
                            result.Append($"\r\n  {x.Item2.ToExplain()} {GetJoinTable()} ON {GetTableName(info.Table)}.{info.Field} {type} {info.Table2.Name}.{info.Field2}");
                            // 收集已用表
                            useTables.Add(info.Table);
                            useTables.Add(info.Table2);
                        }
                        else
                        {
                            result.Append($"\r\n  {info.Prior.ToExplain()} {GetTableName(info.Table)}.{info.Field} {type} {param}");
                        }
                    }
                });
                return result;
            });
        }

        /// <summary>
        /// 获取需要join 的表（取全部表，取已用表，未用过的就是需要join 的表）
        /// todo 这样的计算方式不靠谱
        /// </summary>
        /// <returns></returns>
        private string GetJoinTable()
        {
            var all = allTables.Where(x => x != null).Select(x => x.Name).Distinct();
            var use = useTables.Where(x => x != null).Select(x => x.Name).Distinct();
            var flag = all.Except(use);
            if (flag.Count() == 1)
            {
                return flag.FirstOrDefault();
            }
            throw new Exception("获取join表失败！");
        }

        /// <summary>
        /// 转成top的
        /// </summary>
        /// <param name="top"></param>
        private string ToTop(int top)
        {
            var s = GetSelect();
            if (GetTableInfo().DBType == DBTypeEnum.MySQL)
            {
                return $"{s} {{0}} \r\nLIMIT {top};";
            }

            return $"{s.Replace("SELECT", $"SELECT TOP ({top})")} {{0}};";
        }

        /// <summary>
        /// 获取 having sql 代码
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetHaving()
        {
            return GetSliceSql(SqlTypeEnum.Having,
            () =>
            {
                var result = new StringBuilder("\r\nHAVING 1=1");
                ToWhere(_having, result);
                if (result.ToString() == "\r\nHAVING 1=1")
                    result.Clear();
                return result;
            });
        }
    }
}
