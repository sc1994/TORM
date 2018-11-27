using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace ORM
{
    /// <summary>
    /// 自定义方法
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// NOT IN 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool NotIn<T>(this T field, T[] values) where T : struct
        {
            return true;
        }

        /// <summary>
        /// NOT IN
        /// </summary>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool NotIn(this string field, string[] values)
        {
            return true;
        }

        /// <summary>
        /// NOT IN
        /// </summary>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool NotIn(this DateTime field, DateTime[] values)
        {
            return true;
        }

        /// <summary>
        /// IN
        /// </summary>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In<T>(this T field, T[] values) where T : struct
        {
            return true;
        }

        /// <summary>
        /// IN
        /// </summary>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In(this string field, string[] values)
        {
            return true;
        }

        /// <summary>
        /// IN
        /// </summary>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In(this DateTime field, DateTime[] values)
        {
            return true;
        }

        /// <summary>
        /// COUNT()函数
        /// </summary>
        /// <param name="value">字段信息</param>
        /// <returns></returns>
        public static int Count(object value)
        {
            return 0;
        }

        /// <summary>
        /// MAX()函数
        /// </summary>
        /// <param name="value">字段信息</param>
        /// <returns></returns>
        public static int Max(object value)
        {
            return 0;
        }

        /// <summary>
        /// MIN()函数
        /// </summary>
        /// <param name="value">字段信息</param>
        /// <returns></returns>
        public static int Min(object value)
        {
            return 0;
        }

        /// <summary>
        /// 安全移除
        /// </summary>
        /// <param name="that"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static StringBuilder SafeRemove(this StringBuilder that, int startIndex, int length)
        {
            if (startIndex >= 0 && that.Length >= startIndex + length)
                return that.Remove(startIndex, length);
            return that;
        }

        internal static string GetAppSetting(string key)
        {
            if (Stores.ConfigDic.TryGetValue(key, out var value))
            {
                return value;
            }

            var config = File.ReadAllText("appsettings.json");
            var node = JObject.Parse(config)[key];
            value = node.Value<string>();
            return value;
            //var builder = new ConfigurationBuilder()
            //              .SetBasePath(Directory.GetCurrentDirectory())
            //              .AddJsonFile(fileName);
            //var config = builder.Build();

            //return config.GetSection(key).Value;
        }
    }

    /// <summary>
    /// 事务
    /// </summary>
    public sealed class Transaction
    {
        internal int Sole;

        internal Transaction()
        {
            Sole = GetHashCode();
            Stores.ConnectionDic.TryAdd(Sole, new ConnectionInfo
            {
                Connection = new MySqlConnection()
            });
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public static Transaction Start()
        {
            return new Transaction();
        }

        /// <summary>
        /// 提交操作
        /// </summary>
        public void Commit()
        {
            try
            {
                Stores.ConnectionDic[Sole].Transaction.Commit();
            }
            finally
            {
                Stores.ConnectionDic[Sole].Connection.Close();
            }
        }

        /// <summary>
        /// 回滚操作
        /// </summary>
        public void Rollback()
        {
            try
            {
                Stores.ConnectionDic[Sole].Transaction.Rollback();
            }
            finally
            {
                Stores.ConnectionDic[Sole].Connection.Close();
            }
        }
    }

    /// <summary>
    /// 数据连接
    /// </summary>
    internal class ConnectionInfo
    {
        internal MySqlConnection Connection { get; set; }
        internal MySqlTransaction Transaction { get; set; }
    }
}
