using ORM.Interface.IDelete;
using ORM.Realizes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using StackExchange.Redis;

// todo 尝试收集全部表达式，分组并发解析，提高解析速度
// todo 子查询 where，select 
// todo 运行时抛出不支持的写法的异常
// todo sql 缓存，data 缓存
// todo having 语句只能出现在group
// todo 结构迁移
// todo 慢 sql 监测

namespace ORM
{
    /// <summary>
    /// query 相关
    /// </summary>
    public partial class TORM
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

        #region 多表查询
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
        /// 3表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2> Query<T, TJoin1, TJoin2>()
        {
            return new Realize<T, TJoin1, TJoin2>();
        }

        /// <summary>
        /// 4表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3> Query<T, TJoin1, TJoin2, TJoin3>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3>();
        }

        /// <summary>
        /// 5表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4> Query<T, TJoin1, TJoin2, TJoin3, TJoin4>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4>();
        }

        /// <summary>
        /// 6表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <typeparam name="TJoin5"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5>();
        }

        /// <summary>
        /// 7表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <typeparam name="TJoin5"></typeparam>
        /// <typeparam name="TJoin6"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6>();
        }

        /// <summary>
        /// 8表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <typeparam name="TJoin5"></typeparam>
        /// <typeparam name="TJoin6"></typeparam>
        /// <typeparam name="TJoin7"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7>();
        }

        /// <summary>
        /// 9表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <typeparam name="TJoin5"></typeparam>
        /// <typeparam name="TJoin6"></typeparam>
        /// <typeparam name="TJoin7"></typeparam>
        /// <typeparam name="TJoin8"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8>();
        }

        /// <summary>
        /// 10表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <typeparam name="TJoin5"></typeparam>
        /// <typeparam name="TJoin6"></typeparam>
        /// <typeparam name="TJoin7"></typeparam>
        /// <typeparam name="TJoin8"></typeparam>
        /// <typeparam name="TJoin9"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9>();
        }

        /// <summary>
        /// 11表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <typeparam name="TJoin5"></typeparam>
        /// <typeparam name="TJoin6"></typeparam>
        /// <typeparam name="TJoin7"></typeparam>
        /// <typeparam name="TJoin8"></typeparam>
        /// <typeparam name="TJoin9"></typeparam>
        /// <typeparam name="TJoin10"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10>();
        }

        /// <summary>
        /// 12表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <typeparam name="TJoin5"></typeparam>
        /// <typeparam name="TJoin6"></typeparam>
        /// <typeparam name="TJoin7"></typeparam>
        /// <typeparam name="TJoin8"></typeparam>
        /// <typeparam name="TJoin9"></typeparam>
        /// <typeparam name="TJoin10"></typeparam>
        /// <typeparam name="TJoin11"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11>();
        }

        /// <summary>
        /// 13表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TJoin1"></typeparam>
        /// <typeparam name="TJoin2"></typeparam>
        /// <typeparam name="TJoin3"></typeparam>
        /// <typeparam name="TJoin4"></typeparam>
        /// <typeparam name="TJoin5"></typeparam>
        /// <typeparam name="TJoin6"></typeparam>
        /// <typeparam name="TJoin7"></typeparam>
        /// <typeparam name="TJoin8"></typeparam>
        /// <typeparam name="TJoin9"></typeparam>
        /// <typeparam name="TJoin10"></typeparam>
        /// <typeparam name="TJoin11"></typeparam>
        /// <typeparam name="TJoin12"></typeparam>
        /// <returns></returns>
        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12>();
        }
        #endregion
    }

    /// <summary>
    /// update 相关
    /// </summary>
    public partial class TORM
    {
        /// <summary>
        /// 执行更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static RealizeUpdate<T> Update<T>()
        {
            return new RealizeUpdate<T>();
        }

        /// <summary>
        /// 更新model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static long Update<T>(T model, Transaction transaction = null)
        {
            return new RealizeUpdate<T>().Update(model, transaction);
        }
    }

    /// <summary>
    /// insert 相关 
    /// </summary>
    public partial class TORM
    {
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static long Insert<T>(T model)
        {
            return new RealizeInsert<T>().Insert(model);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        public static long InsertBatch<T>(IEnumerator<T> models)
        {
            return new RealizeInsert<T>().InsertBatch(models);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        public static long InsertBatch<T>(T[] models)
        {
            return new RealizeInsert<T>().InsertBatch(models);
        }
    }

    /// <summary>
    /// delete 相关
    /// </summary>
    public partial class TORM
    {
        /// <summary>
        /// 执行删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static RealizeDelete<T> Delete<T>()
        {
            return new RealizeDelete<T>();
        }

        /// <summary>
        /// 依据主键删除
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <typeparam name="TKey">主键</typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static long Delete<T, TKey>(TKey model)
        {
            return new RealizeDelete<T>().Delete(model);
        }
    }

    /// <summary>
    /// 其他
    /// </summary>
    public partial class TORM
    {
        /// <summary>
        /// 调试模式（会在调用堆栈中，输出sql以及参数信息）
        /// </summary>
        public static bool Debug
        {
            get => Stores.Debug;
            set => Stores.Debug = value;
        }

        /// <summary>
        /// 使用redis收集log信息（需要自己消费）
        /// </summary>
        public static ConnectionMultiplexer RedisLog
        {
            get => Stores.RedisLog;
            set => Stores.RedisLog = value;
        }

        /// <summary>
        /// 自动生成表（迁移模式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void AutoTable<T>()
        {
            new Others<T>().AutoTable();
        }
    }
}

