using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ORM
{

    /// <summary>
    /// 存储实例运行中不会变化的值
    /// </summary>
    public static class Stores
    {
        /// <summary>
        /// 表信息
        /// </summary>
        internal static ConcurrentDictionary<string, TableInfo> TableInfoDic => new ConcurrentDictionary<string, TableInfo>();

        /// <summary>
        /// 存放连接和事务
        /// </summary>
        internal static ConcurrentDictionary<int, ConnectionInfo> Connections => new ConcurrentDictionary<int, ConnectionInfo>();
    }

    internal class TableInfo
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
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
