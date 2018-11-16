using Explain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Realizes
{
    /// <summary>
    /// 一些解析的通用方法
    /// </summary>
    public class RealizeToSql<T>
    {
        /// <summary>
        /// 存放 join 表达式
        /// </summary>
        protected List<(Expression, JoinEnum)> _join = new List<(Expression, JoinEnum)>();
        /// <summary>
        /// 存放 select 表达式
        /// </summary>
        protected List<Expression> _selects = new List<Expression>();
        /// <summary>
        /// 存放 有别名的 select 表达式
        /// </summary>
        protected List<(Expression, string)> _selectAlias = new List<(Expression, string)>();
        /// <summary>
        /// 存放 order 表达式
        /// </summary>
        protected List<(Expression, OrderEnum)> _orders = new List<(Expression, OrderEnum)>();
        /// <summary>
        /// 存放 group 表达式
        /// </summary>
        protected List<Expression> _groups = new List<Expression>();
        /// <summary>
        /// 存放 where 表达式
        /// </summary>
        protected List<Expression> _where = new List<Expression>();
        /// <summary>
        /// 存放 having 表达式
        /// </summary>
        protected List<Expression> _having = new List<Expression>();
        /// <summary>
        /// 存放 set 表达式
        /// </summary>
        protected List<(Expression, object)> _set = new List<(Expression, object)>();
        /// <summary>
        /// 存放参数
        /// </summary>
        protected Dictionary<string, object> _params = new Dictionary<string, object>();
        /// <summary>
        /// 获取 T 属性，避免每次都计算
        /// </summary>
        protected Type _t = typeof(T);
        /// <summary>
        /// 已经用到的表（为了筛选当前 join 的那个表）
        /// </summary>
        protected List<Type> useTables = new List<Type> { typeof(T) };
        /// <summary>
        /// 全部的表
        /// </summary>
        protected List<Type> allTables = new List<Type>();
        /// <summary>
        /// 表信息
        /// </summary>
        private readonly Dictionary<string, TableInfo> _tableInfoDic = new Dictionary<string, TableInfo>();
        /// <summary>
        /// sql（为了防止多次调用同一个方法而多次解析，将sql存放在这边）
        /// </summary>
        private readonly Dictionary<SqlTypeEnum, StringBuilder> _sqlDic = new Dictionary<SqlTypeEnum, StringBuilder>();

        /// <summary>
        /// 获取 having sql 代码
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetHaving()
        {
            return GetSliceSql(SqlTypeEnum.Having,
            () =>
            {
                var result = new StringBuilder("\r\nHAVING 1=1");
                ToWhere(_having, result);
                return result;
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
                var result = new StringBuilder("\r\nWHERE 1=1");
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
                        _params.Add(param, x.Value);
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
        /// 获取 select sql 代码
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetSelect()
        {
            return GetSliceSql(SqlTypeEnum.Select, () =>
            {
                var result = new StringBuilder("SELECT");
                _selects.ForEach(x => ToSelect(x, null, result));
                _selectAlias.ForEach(x => ToSelect(x.Item1, x.Item2, result));
                if (result.ToString() != "SELECT")
                {
                    result.TryRemove(result.Length - 1, 1);
                }
                else
                {
                    result.Append(" *");
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
        /// 获取 join sql 代码
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetJoin()
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
                 return result.TryRemove(result.Length - 1, 1);
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
                        result.Append($"\r\n  {GetTableName(info.Table)}.{info.Field} {item.Item2.ToExplain()}");
                    }
                });
                if (result.Length > 0)
                {
                    result.Insert(0, "\r\nORDER BY");
                }
                return result.TryRemove(result.Length - 1, 1);
            });
        }

        /// <summary>
        /// 获取 set sql 代码
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetSet()
        {
            return GetSliceSql(SqlTypeEnum.Order,
            () =>
            {
                var result = new StringBuilder("\r\nSET");

                throw new NotImplementedException("待实现");
            });
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
            ExplainTool.Log("GetSliceSql", $"记录GetSliceSql({type})计算频率。");
            // 字典中没有相应的值，执行委托
            var result = func();
            // 存入字典，以备下一次调用
            _sqlDic.Add(type, result);
            return result;
        }

        /// <summary>
        /// 获取需要join 的表（取全部表，取已用表，未用过的就是需要join 的表）
        /// todo 这样的计算方式不靠谱
        /// </summary>
        /// <returns></returns>
        protected string GetJoinTable()
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
        /// 取主表
        /// </summary>
        /// <returns></returns>
        protected string GetTableName()
        {
            return GetTableName(_t);
        }

        /// <summary>
        /// 取指定表名
        /// </summary>
        /// <returns></returns>
        protected string GetTableName(Type table)
        {
            return GetTableInfo(table).Table;
        }

        /// <summary>
        /// 转成分页的
        /// </summary>
        /// <param name="index"></param>
        /// <param name="size"></param>
        protected string ToPage(int index, int size)
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
                return $"{select}{{0}}\r\nLIMIT{(index - 1) * size + 1},{index * size}";
            }
            else
            {
                throw new NotImplementedException("未实现的数据库类型");
            }
        }

        /// <summary>
        /// 转成top的
        /// </summary>
        /// <param name="top"></param>
        protected string ToTop(int top)
        {
            var s = GetSelect();
            if (GetTableInfo().DBType == DBTypeEnum.MySQL)
            {
                return $"{s} {{0}} \r\nLIMIT {top};";
            }

            return $"{s.Replace("SELECT", $"SELECT TOP ({top})").Replace("UPDATE", $"UPDATE TOP ({top})")} {{0}};";
        }

        /// <summary>
        /// 依据特性获取表信息
        /// </summary>
        /// <returns></returns>
        private TableInfo GetTableInfo()
        {
            return GetTableInfo(_t);
        }

        /// <summary>
        /// 依据特性获取表信息
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private TableInfo GetTableInfo(Type table)
        {
            if (_tableInfoDic.ContainsKey(table.Name))
            {
                return _tableInfoDic[table.Name];
            }

            ExplainTool.Log("GetTableInfo", $"记录GetTableInfo({table.Name})计算频率");

            var attribute = table.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();

            if (attribute == null)
            {
                throw new Exception("需指定表的特性，使用 TableAttribute 。");
            }

            var info = (TableAttribute)attribute;
            var r = new TableInfo
            {
                DB = info.DB,
                DBType = info.DBType,
                Table = info.Table
            };
            _tableInfoDic.Add(table.Name, r);
            return r;
        }
    }

    class TableInfo
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public string DB { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DBTypeEnum DBType { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Table { get; set; }
    }
}
