using ORM.Interface;
using ORM.Realizes;
using System;

// todo 尝试收集全部表达式，分组并发解析，提高解析速度
// todo 子查询 where，select 
// todo 事务
// todo 运行时抛出不支持的写法的异常
// todo 收集sql存起来以备二次调用，无需每次调用方法都去解析一次表达式
// todo sql 缓存，data 缓存
// todo having 语句

namespace ORM
{
    /// <summary>
    /// query 相关
    /// </summary>
    public partial class ORM
    {
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Realize<T> Query<T>()
        {
            return new Realize<T>();
        }

        /// <summary>
        /// 两表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin> Query<T, TJoin>()
        {
            return new Realize<T, TJoin>();
        }

        /// <summary>
        /// 三表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2> Query<T, TJoin1, TJoin2>()
        {
            return new Realize<T, TJoin1, TJoin2>();
        }
        // todo 多join的实现
    }

    /// <summary>
    /// update 相关
    /// </summary>
    public partial class ORM
    {
        public static IUpdateSet<T> Update<T>()
        {
            throw new NotImplementedException();
        }

        public static bool Update<T>(T model)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// insert相关
    /// </summary>
    public partial class ORM
    {
        public static int Insert<T>(params T[] models)
        {
            throw new NotImplementedException();
        }

        public static long Insert<T>(T model)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// delete 相关
    /// </summary>
    public partial class ORM
    {
        public static bool Delete<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }
    }
}
