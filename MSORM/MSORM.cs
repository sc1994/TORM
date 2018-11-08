using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MSORM
{
    public class MSORM<T> : IMSORM<T>
    {

    }

    public interface IMSORM<T> : ISelect<T>
    {

    }

    public interface ISelect<T> : IJoin<T>
    {
        ISelect<T> Select<TValue>(Expression<Func<T, TValue>> exp);
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

    public interface IWhere<T> : IOrder<T>
    {
        IWhere<T> Where<TValue>(Expression<Func<T, bool>> exp);
        IWhere<T> Where<TValue>(Expression<Func<T, bool[]>> exp);
        IWhere<T> In<TValue>(Expression<Func<T, TValue>> exp, IEnumerable<TValue> values);
        IWhere<T> NotIn<TValue>(Expression<Func<T, TValue>> exp, IEnumerable<TValue> values);
        IWhere<T> LikeF<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> LikeL<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> LikeR<TValue>(Expression<Func<T, TValue>> exp, TValue value);
    }

    public interface IOrder<T> : IMethod<T>
    {
        IOrder<T> OrderA(params Expression<Func<T, object>>[] exps);
        IOrder<T> OrderD(params Expression<Func<T, object>>[] exps);
    }

    public interface IMethod<T>
    {
        IEnumerable<T> List();
        bool Exist();
    }


    public class Test
    {
        public string Name { get; set; }

        public long Id { get; set; }

        public void Xxx()
        {
            //var t = new MSORM<Test>()
            //    .Select(x => new object[] { x.Name, x.Id })
            //    .Join<Test2>(x => x.Name, x => x.Name2)

            //    //.Join<Test2>((x, y) => x.Name == y.Name2)

            //    //.Eq(x => new[] { x.Name == "11", x.Name == "22" })
            //    .OrLt(x => x.Name, "123")
            //    .Asc(x => x.Name, x => x.Name)
            //    .Desc(x => x.Name)
            //    .List();
        }
    }



    public class Test2
    {
        public string Name2 { get; set; }
    }
}
