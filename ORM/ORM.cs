using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ORM.Interface;

// todo 尝试收集全部表达式，分组并发解析，提高解析速度
// todo "join a on a.id = id and xxx" and后面的语法
// todo 子查询 where，select 

namespace ORM
{
    public class ORM
    {
        public static ISelect<T> Query<T>()
        {
            throw new NotImplementedException();
        }

        public static ISelect<T, TJoin> Query<T, TJoin>()
        {
            throw new NotImplementedException();
        }

        public static ISelect<T, TJoin1, TJoin2> Query<T, TJoin1, TJoin2>()
        {
            throw new NotImplementedException();
        }

        public static ISelect<T, TJoin1, TJoin2, TJoin3> Query<T, TJoin1, TJoin2, TJoin3>()
        {
            throw new NotImplementedException();
        }


        public static ISelect<T, TJoin1, TJoin2, TJoin3, TJoin4> Query<T, TJoin1, TJoin2, TJoin3, TJoin4>()
        {
            throw new NotImplementedException();
        }

        public static IUpdateSet<T> Update<T>()
        {
            throw new NotImplementedException();
        }
    }




    public class Test
    {
        public string Name { get; set; }

        public long Id { get; set; }

        public DateTime Date { get; set; }

        public void Xxx()
        {
            //var data = new ORM<Test>()
            //    .Select(x => x.Name, x => x.Id)
            //    .Select(x => new[] { x.Name, x.Name })
            //    .Where(x => x.Name == "123", x => x.Id == 3)
            //    .Where(x => x.Date == DateTime.Now.Date)
            //    .Where(x => new[] { x.Name == "123", x.Id == 2, x.Name == "123" })
            //    .In(x => x.Id, new List<long> { 1, 2, 34, 5 })
            //    .LikeF(x => x.Name, "s")
            //    .OrderA(x => x.Name, x => x.Id)
            //    .Page(1, 3);

            var data = ORM.Query<Test, Test2>()
                .Select((x, y) => new object[] { x.Name, x.Date })
                .Select((x, y) => ORMTool.Max(x.Id))
                .Join((x, y) => x.Name == y.Name2)
                .And((x, y) => x.Name == "123" || x.Date == DateTime.Today)
                .OrderA((x, y) => x.Name)
                .Find<TestAndTest2>();

            //var a = data.data;
            //var b = data.total;
        }
    }

    public class TestAndTest2
    {

    }

    public class Test2
    {
        public string Name2 { get; set; }
    }
}
