using StackExchange.Redis;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ORM
{
    /// <summary>
    /// 存储实例运行中不会变化的值
    /// </summary>
    internal static class Stores
    {
        /// <summary>
        /// 表信息
        /// </summary>
        internal static ConcurrentDictionary<int, TableInfo> TableInfoDic { get; } = new ConcurrentDictionary<int, TableInfo>();

        /// <summary>
        /// 存放字段信息
        /// </summary>
        internal static ConcurrentDictionary<int, FieldInfo> FieldInfoDic { get; } = new ConcurrentDictionary<int, FieldInfo>();

        /// <summary>
        /// 存放连接和事务
        /// </summary>
        internal static ConcurrentDictionary<int, ConnectionInfo> ConnectionDic { get; } = new ConcurrentDictionary<int, ConnectionInfo>();

        /// <summary>
        /// 存放不会变化的sql，比如数据插入sql
        /// </summary>
        internal static ConcurrentDictionary<string, string> SqlDic { get; } = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 调试模式
        /// </summary>
        internal static bool Debug { get; set; } = false;

        /// <summary>
        /// 调试模式
        /// </summary>
        internal static ConnectionMultiplexer RedisLog { get; set; } = null;

        /// <summary>
        /// 存放一些项目配置
        /// </summary>
        internal static Dictionary<string, string> DbConfigDic { get; } = new Dictionary<string, string>();
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
        public string Name { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public FieldInfo Key { get; set; }
        /// <summary>
        /// 自增键
        /// </summary>
        public FieldInfo Identity { get; set; }
    }

    /// <summary>
    /// 字段信息
    /// </summary>
    internal class FieldInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 不可为空
        /// </summary>
        public bool NotNull { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool Key { get; set; }
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool Identity { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        internal int Length { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        internal int Precision { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        internal string Type { get; set; }
        /// <summary>
        /// 外键
        /// </summary>
        internal string Foreign { get; set; }
    }

    /// <summary>
    /// 来自数据库的字段信息
    /// </summary>
    internal class FieldByDB : FieldInfo
    {
        /// <summary>
        /// 用于数据库读取表信息
        /// </summary>
        internal int StringLength { get; set; }
        /// <summary>
        /// 精度，解决关键字冲突
        /// </summary>
        internal int PrecisionDB { get; set; }
        /// <summary>
        /// 自增键
        /// </summary>
        internal string IdentityDB { get; set; }
        internal string NotNullDB { get; set; }
    }
}
