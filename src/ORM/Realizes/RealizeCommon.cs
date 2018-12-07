﻿using Dapper;
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

        #region 时间埋点
        /// <summary>
        /// 记录开始时间
        /// </summary>
        protected DateTime _starTime { get; set; }
        /// <summary>
        /// 解释时长
        /// </summary>
        protected TimeSpan _explainSpan { get; set; }
        /// <summary>
        /// 连接时长
        /// </summary>
        protected TimeSpan _connSpan { get; set; }
        /// <summary>
        /// 执行时长
        /// </summary>
        protected TimeSpan _executeSpan { get; set; }
        #endregion

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

            var attribute = table.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();

            if (attribute == null)
            {
                throw new Exception("需指定表的特性，使用 TableAttribute 。");
            }

            var info = (TableAttribute)attribute;
            var fields = typeof(T).GetProperties().Select(GetFieldInfo);
            var r = new TableInfo
            {
                DB = info.DB,
                DBType = info.DBType,
                Table = string.IsNullOrWhiteSpace(info.Table) ? table.Name : info.Table,
                ConnectionString = Stores.DbConfigDic[info.DB],
                Key = fields.FirstOrDefault(x => x.Key),
                Identity = fields.FirstOrDefault(x => x.Identity)
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
            var foreign = field.GetCustomAttributes(typeof(ForeignAttribute), true).FirstOrDefault();
            if (fieldInfo is FieldAttribute value)
            {
                result.Name = string.IsNullOrWhiteSpace(value.Alias) ? field.Name : value.Alias;
                result.Comment = value.Comment;
                result.DefaultValue = value.DefaultValue;
                result.NotNull = value.NotNull;
                result.Length = value.Length;
                result.Precision = value.Precision;
            }
            else if (foreign is ForeignAttribute fValue)
            {
                result.Foreign = fValue.Foreign;
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
        protected long Execute(string sql, Transaction transaction = null, object param = null)
        {
            _explainSpan = DateTime.Now - _starTime;
            _starTime = DateTime.Now;
            // 决定是使用默认参数还是传入参数
            var thatParam = param ?? _params;
            try
            {
                // 事务操作
                if (transaction != null)
                {
                    var value = Stores.ConnectionDic[transaction.Sole];
                    if (value.Transaction == null) // 希望在 事务开始的时候尽量少的参数，所以连接的开启放在了这边
                    {
                        value.Connection.ConnectionString = GetTableInfo().ConnectionString;
                        value.Connection.Open();
                        value.Transaction = value.Connection.BeginTransaction(); // 涉及到对字典中的值进行变动，不能预知当高并发的情况下，是否会产生问题。
                    }
                    _connSpan = DateTime.Now - _starTime;
                    _starTime = DateTime.Now;
                    return value.Connection.Execute(sql, thatParam, Stores.ConnectionDic[transaction.Sole].Transaction);
                }
                // 非事务
                MySqlConnection connection;
                using (connection = new MySqlConnection(GetTableInfo().ConnectionString))
                {
                    return connection.Execute(sql, thatParam);
                }
            }
            catch (Exception ex)
            {
                LogSql(sql, _params, ex);
                throw;
            }
            finally
            {
                LogSql(sql, _params);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected TOther QueryFirst<TOther>(string sql, object param = null)
        {
            _explainSpan = DateTime.Now - _starTime;
            _starTime = DateTime.Now;
            var thatParam = param ?? _params;
            try
            {
                using (var connection = new MySqlConnection(GetTableInfo().ConnectionString))
                {
                    _connSpan = DateTime.Now - _starTime;
                    _starTime = DateTime.Now;
                    return connection.QueryFirstOrDefault<TOther>(sql, thatParam);
                }
            }
            catch (Exception ex)
            {
                LogSql(sql, thatParam, ex);
                throw;
            }
            finally
            {
                LogSql(sql, thatParam);
            }
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected IEnumerable<TOther> Query<TOther>(string sql, object param = null)
        {
            _explainSpan = DateTime.Now - _starTime;
            _starTime = DateTime.Now;
            var thatParam = param ?? _params;
            try
            {
                using (var connection = new MySqlConnection(GetTableInfo().ConnectionString))
                {
                    _connSpan = DateTime.Now - _starTime;
                    _starTime = DateTime.Now;
                    return connection.Query<TOther>(sql, thatParam);
                }
            }
            catch (Exception ex)
            {
                LogSql(sql, thatParam, ex);
                throw;
            }
            finally
            {
                LogSql(sql, thatParam);
            }
        }

        /// <summary>
        /// 打印 sql 到堆栈
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="ex"></param>
        protected void LogSql(string sql, object param, Exception ex = null)
        {
            if (!Stores.Debug && Stores.RedisLog == null) return;
            _executeSpan = DateTime.Now - _starTime;

            var info = new
            {
                SqlStr = sql,
                Param = JsonConvert.SerializeObject(param, Formatting.Indented),
                StackTrace = new StackTrace(true).ToString(),
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ExplainSpan = _explainSpan.TotalMilliseconds,
                ConnectSpan = _connSpan.TotalMilliseconds,
                ExecuteSpan = _executeSpan.TotalMilliseconds,
                ExMessage = ex?.Message ?? "",
                DbName = GetTableInfo().DB,
                TableName = string.Join(",", useTables.Select(GetTableName).Distinct().OrderBy(x => x))
            };
            if (Stores.Debug)
            {
                Trace.WriteLine(
$@"
===========>SQL<============
{info.SqlStr}
===========>参数<============
{info.Param}
===========>堆栈<============
{info.StackTrace}
===========>耗时<============
解析：{info.ExplainSpan}ms
连接：{info.ConnectSpan}ms
执行：{info.ExecuteSpan}ms
===>{info.EndTime}<===
"
                    );
            }
            if (Stores.RedisLog != null)
            {
                Redis.Publish("LogSql", info);
            }
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

        private DateTime SwitchTime(MemberInfo member)
        {
            var arr = member.ToString().Split(' ');
            if (arr.Length > 1)
            {
                switch (arr[1].ToLower())
                {
                    case "now": return DateTime.Now;
                    case "today": return DateTime.Today;
                    case "maxvalue": return DateTime.MaxValue;
                    case "minvalue": return DateTime.MinValue;
                    case "utcnow": return DateTime.UtcNow;
                }
                throw new NotImplementedException("意料之外的DateTime常量");
            }
            throw new NotImplementedException("意料之外的member数据");
        }
    }
}