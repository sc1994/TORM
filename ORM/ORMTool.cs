using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
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
            if (startIndex >= 0 && that.Length >= startIndex + length)
                return that.Remove(startIndex, length);
            return that;
        }

        internal static T GetConfigJson<T>(string key, string fileName = "appsettings.json") where T : class, new()
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile(fileName);
            var config = builder.Build();

            var entity = new T();
            config.GetSection(key).Bind(entity);
            return entity;
        }

        internal static string GetAppSetting(string key, string fileName = "appsettings.json")
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile(fileName);
            var config = builder.Build();

            return config.GetSection(key).Value;
        }
    }

    /// <summary>
    /// 事务
    /// </summary>
    public sealed class Transaction
    {
        internal int Sole;

        /// <summary>
        /// 存放连接和事务
        /// </summary>
        internal static Dictionary<int, (MySqlConnection connection, MySqlTransaction transaction)> Connections = new Dictionary<int, (MySqlConnection connection, MySqlTransaction transaction)>();

        public Transaction()
        {
            var con = new MySqlConnection();
            Sole = GetHashCode();
            Connections.Add(Sole, (con, null));
        }

        public static Transaction Start()
        {
            return new Transaction();
        }

        public void Commit()
        {
            try
            {
                Connections[Sole].transaction.Commit();
            }
            finally
            {
                Connections[Sole].connection.Close();
            }
        }

        public void Rollback()
        {
            try
            {
                Connections[Sole].transaction.Rollback();
            }
            finally
            {
                Connections[Sole].connection.Close();
            }
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
