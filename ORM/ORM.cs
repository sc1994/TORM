using ORM.Interface.IDelete;
using ORM.Realizes;
using System;
using System.Collections.Generic;
using System.Text;

// todo 尝试收集全部表达式，分组并发解析，提高解析速度
// todo 子查询 where，select 
// todo 事务
// todo 运行时抛出不支持的写法的异常
// todo 收集sql存起来以备二次调用，无需每次调用方法都去解析一次表达式
// todo sql 缓存，data 缓存
// todo having 语句只能出现在group
// todo 结构迁移
// todo 慢 sql 监测

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

        #region 更多表查询
        public static Realize<T, TJoin1, TJoin2> Query<T, TJoin1, TJoin2>()
        {
            return new Realize<T, TJoin1, TJoin2>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3> Query<T, TJoin1, TJoin2, TJoin3>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4> Query<T, TJoin1, TJoin2, TJoin3, TJoin4>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11>();
        }

        public static Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12> Query<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12>()
        {
            return new Realize<T, TJoin1, TJoin2, TJoin3, TJoin4, TJoin5, TJoin6, TJoin7, TJoin8, TJoin9, TJoin10, TJoin11, TJoin12>();
        }
        #endregion
    }

    /// <summary>
    /// update 相关
    /// </summary>
    public partial class ORM
    {
        public static RealizeUpdate<T> Update<T>()
        {
            return new RealizeUpdate<T>();
        }

        public static int Update<T>(T model, Transaction transaction = null)
        {
            return new RealizeUpdate<T>().Update(model, transaction);
        }
    }

    /// <summary>
    /// insert 相关  todo
    /// </summary>
    public partial class ORM
    {
        public static int Insert<T>(T model)
        {
            return new RealizeInsert<T>().Insert(model);
        }

        public static int InsertBatch<T>(IEnumerator<T> models)
        {
            return new RealizeInsert<T>().InsertBatch(models);
        }
    }

    /// <summary>
    /// delete 相关
    /// </summary>
    public partial class ORM
    {
        public IDelete<T> Delete<T>()
        {
            return new RealizeDelete<T>();
        }

        public int Delete<T>(long id)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 其他
    /// </summary>
    public partial class ORM
    {
        public static bool Debug
        {
            get => Stores.Debug;
            set => Stores.Debug = value;
        }

        public static void AutoTable<T>()
        {
            throw new NotImplementedException();
        }
    }
}

