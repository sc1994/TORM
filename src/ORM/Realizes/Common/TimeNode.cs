using Explain;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;

namespace ORM.Realizes
{
    /// <summary>
    /// 时间节点
    /// </summary>
    // ReSharper disable once UnusedTypeParameter
    public partial class RealizeCommon<T>
    {
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
                StackTrace = new System.Diagnostics.StackTrace(true).ToString(),
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
    }
}
