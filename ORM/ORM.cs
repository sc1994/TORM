using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ORM
{
    //public class ORM<T> : IORM<T>
    //{
    // todo 尝试收集全部表达式，分组并发解析，提高解析速度
    //}

    public interface IORM<T> : ISelect<T>
    {
        // todo 考虑使用高级点的办法初始化实体指定泛型，而不是单纯的new
    }

    public static class ORMTool
    {
        public static bool In<TValue>(this object field, IEnumerable<TValue> values)
        {
            return true;
        }
        public static bool LikeF<TValue>(this string field, string value)
        {
            return true;
        }
        public static bool LikeR<TValue>(this string field, string value)
        {
            return true;
        }
        public static bool LikeL<TValue>(this string field, string value)
        {
            return true;
        }
    }

    public interface ISelect<T> : IJoin<T>
    {
        ISelect<T> Select(params Expression<Func<T, object>>[] exps);

        ISelect<T> Select<TValue>(Expression<Func<T, TValue>> exp, string alias);
        ISelect<T> Select<TValue>(params (Expression<Func<T, object>> exp, string alias)[] exps);

        ISelect<T> Select<TValue>(Expression<Func<T, TValue[]>> exps);
        ISelect<T> Select(Expression<Func<T, object[]>> exps);
    }

    public interface IJoin<T> : IWhere<T>
    {
        IJoin<T> Join<TJoin>(Expression<Func<T, TJoin, bool>> exp); // todo 扩展支持 "join a on a.id = id and xxx" and后面的语法
        IJoin<T> JoinL<TLJoin>(Expression<Func<T, TLJoin, bool>> exp);
        IJoin<T> JoinR<TRJoin>(Expression<Func<T, TRJoin, bool>> exp);
        IJoin<T> JoinF<TFJoin>(Expression<Func<T, TFJoin, bool>> exp);
    }

    public interface IWhere<T> : IOrder<T>, IUpdate
    {
        IWhere<T> And(Expression<Func<T, bool>> exp);
        IWhere<T> Or(Expression<Func<T, bool>> exp);
    }

    public interface IOrder<T> : IMethod<T>
    {
        IOrder<T> OrderA<TValue>(params Expression<Func<T, TValue>>[] exps);
        IOrder<T> OrderA(params Expression<Func<T, object>>[] exps);
        IOrder<T> OrderD(params Expression<Func<T, object>>[] exps);
    }

    public interface IMethod<T>
    {
        bool Exist();
        T First();
        TOther First<TOther>();
        IEnumerable<TOther> Find<TOther>();
        IEnumerable<T> Find();
        (IEnumerable<T> data, int total) Page(int index, int size);
        (IEnumerable<TOther> data, int total) Page<TOther>(int index, int size);
    }

    public interface ISet<T>
    {
        ISet<T> Set<TValue>(Expression<Func<T, TValue>> exp, string value);
        ISet<T> Set<TValue>(params (Expression<Func<T, object>> exp, string value)[] exps);
    }

    public interface IUpdate
    {
        int Update(int top = 0);
        List<UpdateRecord> Update(int top = 0, bool record = false);
    }

    public class UpdateRecord
    {
        public string Field { get; set; }

        public object Old { get; set; }

        public object New { get; set; }
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

            //var a = data.data;
            //var b = data.total;
        }
    }



    public class Test2
    {
        public string Name2 { get; set; }
    }
}
