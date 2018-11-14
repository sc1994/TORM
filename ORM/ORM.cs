using ORM.Interface;
using ORM.Realizes;
using System;

// todo 尝试收集全部表达式，分组并发解析，提高解析速度
// todo 子查询 where，select 
// todo 事务
// todo 运行时抛出不支持的写法的异常

namespace ORM
{
    /// <summary>
    /// query 相关
    /// </summary>
    public partial class ORM
    {
        public static Realize<T> Query<T>()
        {
            return new Realize<T>();
        }

        public static Realize<T, TJoin> Query<T, TJoin>()
        {
            return new Realize<T, TJoin>();
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
