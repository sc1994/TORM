using Dapper;
using Explain;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ORM.Realizes
{
    /// <summary>
    /// 一些解析的通用方法
    /// </summary>
    public class RealizeCommon<T>
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
        /// sql（为了防止多次调用同一个方法而多次解析，将sql存放在这边）
        /// </summary>
        private readonly Dictionary<SqlTypeEnum, StringBuilder> _sqlDic = new Dictionary<SqlTypeEnum, StringBuilder>();

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
        /// 依据特性获取表信息
        /// </summary>
        /// <returns></returns>
        internal TableInfo GetTableInfo()
        {
            return GetTableInfo(_t);
        }

        /// <summary>
        /// 依据特性获取表信息
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        internal TableInfo GetTableInfo(Type table)
        {
            if (Stores.TableInfoDic.TryGetValue(table.MetadataToken, out var value))
            {
                return value;
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
                Table = string.IsNullOrWhiteSpace(info.Table) ? table.Name : info.Table,
                ConnectionString = Tools.GetAppSetting(info.DB)
            };
            Stores.TableInfoDic.TryAdd(table.MetadataToken, r);
            return r;
        }

        /// <summary>
        /// 获取字段属性
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        internal FieldInfo GetFieldInfo(PropertyInfo field)
        {
            if (Stores.FieldInfoDic.TryGetValue(field.MetadataToken, out var result))
            {
                return result;
            }

            result = new FieldInfo
            {
                Key = field.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() != null,
                Identity = field.GetCustomAttributes(typeof(IdentityAttribute), true).FirstOrDefault() != null,
                Type = field.PropertyType.Name.ToLower()
            };
            var fieldInfo = field.GetCustomAttributes(typeof(FieldAttribute), true).FirstOrDefault();
            if (fieldInfo is FieldAttribute value)
            {
                result.Name = string.IsNullOrWhiteSpace(value.Alias) ? field.Name : value.Alias;
                result.Comment = value.Comment;
                result.DefaultValue = value.DefaultValue;
                result.NotNull = value.NotNull;
                result.Length = value.Length;
                result.Precision = value.Precision;
            }

            if (string.IsNullOrWhiteSpace(result.Name))
            {
                result.Name = field.Name;
            }
            Stores.FieldInfoDic.TryAdd(field.MetadataToken, result);
            return result;
        }

        /// <summary>
        /// 执行sql，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected int Execute(string sql, Transaction transaction = null, object param = null)
        {
            MySqlConnection connection;
            // 决定是使用默认参数还是传入参数
            var thatParam = param ?? _params;
            LogSql(sql, thatParam);
            if (transaction != null)
            {
                var value = Stores.ConnectionDic[transaction.Sole];
                if (value.Transaction == null) // 希望在 事务开始的时候尽量少的参数，所以连接的开启放在了这边
                {
                    value.Connection.ConnectionString = GetTableInfo().ConnectionString;
                    value.Connection.Open();
                    value.Transaction = value.Connection.BeginTransaction(); // 涉及到对字典中的值进行变动，不能预知当高并发的情况下，是否会产生问题。
                }
                return value.Connection.Execute(sql, thatParam, Stores.ConnectionDic[transaction.Sole].Transaction);
            }

            using (connection = new MySqlConnection(GetTableInfo().ConnectionString))
            {
                return connection.Execute(sql, thatParam);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected TOther QueryFirst<TOther>(string sql)
        {
            LogSql(sql, _params);
            using (var connection = new MySqlConnection(GetTableInfo().ConnectionString))
            {
                return connection.QueryFirst<TOther>(sql, _params);
            }
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected IEnumerable<TOther> Query<TOther>(string sql)
        {
            LogSql(sql, _params);
            using (var connection = new MySqlConnection(GetTableInfo().ConnectionString))
            {
                return connection.Query<TOther>(sql, _params);
            }
        }

        protected void LogSql(string sql, object param)
        {
            if (!Stores.Debug) return;
            Trace.WriteLine(
$@"

================SQL>{DateTime.Now:u}<SQL================
{sql}
==============Param>{DateTime.Now:u}<Param==============
{JsonConvert.SerializeObject(param, Formatting.Indented)}
================End>{DateTime.Now:u}<End================

");
        }

        /// <summary>
        /// 验证T且返回类型
        /// </summary>
        /// <returns></returns>
        protected Type ChenkT()
        {
            var type = typeof(T);
            if (type.IsArray)
            {
                throw new Exception("勿使用嵌套数组");
            }
            return type;
        }
    }
}
