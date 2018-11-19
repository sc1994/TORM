using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ORM
{
    /// <summary>
    /// 自定义方法
    /// </summary>
    public static class ORMTool
    {
        public static bool NotIn<T>(this T field, T[] values) where T : struct
        {
            return true;
        }

        public static bool NotIn(this string field, string[] values)
        {
            return true;
        }
        public static bool NotIn(this DateTime field, DateTime[] values)
        {
            return true;
        }

        public static bool In<T>(this T field, T[] values) where T : struct
        {
            return true;
        }
        public static bool In(this string field, string[] values)
        {
            return true;
        }
        public static bool In(this DateTime field, DateTime[] values)
        {
            return true;
        }

        public static int Count(object value)
        {
            return 0;
        }
        public static int Max(object value)
        {
            return 0;
        }
        public static int Min(object value)
        {
            return 0;
        }

        public static StringBuilder TryRemove(this StringBuilder that, int startIndex, int length)
        {
            if (that.Length > startIndex + length)
                return that.Remove(startIndex, length);
            return that;
        }
    }

    /// <summary>
    /// 事务
    /// </summary>
    public class Transaction
    {
        internal string Sole;

        /// <summary>
        /// 存放连接和事务
        /// </summary>
        internal static Dictionary<string, (MySqlConnection connection, MySqlTransaction transaction)> Connections = new Dictionary<string, (MySqlConnection connection, MySqlTransaction transaction)>();

        public Transaction()
        {
            var con = new MySqlConnection();
            Sole = MD5.Create().ToString();
            Connections.Add(Sole, (con, con.BeginTransaction()));
        }

        public static Transaction Start()
        {
            return new Transaction();
        }

        public void Commit()
        {
            // todo 
        }

        public void Rollback()
        {
            // todo 
        }
    }

    /// <summary>
    /// 表属性标记
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public string DB { get; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DBTypeEnum DBType { get; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Table { get; }

        public TableAttribute(string db, DBTypeEnum dbType)
        {
            DB = db;
            DBType = dbType;
        }

        public TableAttribute(string db, DBTypeEnum dbType, string table)
        {
            DB = db;
            DBType = dbType;
            Table = table;
        }
    }
}
