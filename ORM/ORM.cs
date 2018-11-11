using ORM.Interface;
using System;
using ORM.Realizes;

// todo 尝试收集全部表达式，分组并发解析，提高解析速度
// todo 子查询 where，select 
// todo 事务
// todo 运行时抛出不支持的写法的异常

namespace ORM
{
    public class ORM
    {
        public static Realize<T> Query<T>()
        {
            return new Realize<T>();
        }

        public static Realize<T, TJoin> Query<T, TJoin>()
        {
            return new Realize<T, TJoin>();
        }

        public static IUpdateSet<T> Update<T>()
        {
            throw new NotImplementedException();
        }

        public static int Insert<T>(params T[] models)
        {
            throw new NotImplementedException();
        }

        public static long Insert<T>(T model)
        {
            throw new NotImplementedException();
        }
    }

    public class Test
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public void Query()
        {
            //ORM.Query<Test>()
            //    .Select(x => x.Id)
            //    .And(x => x.Name == "1")
            //    .OrderA(x => x.Id)
            //    .Group(x => x.Name)
            //    .Find();
        }
    }
}
