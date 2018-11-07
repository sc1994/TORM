using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MSORM
{
    public class MSORM<T> : IMSORM<T>
    {

        public IWhere<T> Eq<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> Equal<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> Gt<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> GreatThan<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> Gte<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> GreatThanEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> Lt<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> LessThan<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> Lte<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> LessThanEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> Like<TValue>(Expression<Func<T, TValue>> exp, TValue value, LikeEnum way)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> In<TValue>(Expression<Func<T, TValue>> exp, IList<TValue> value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrEq<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrGt<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrGreatThan<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrGte<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrGreatThanEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrLt<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrLessThan<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrLte<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrLessThanEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrLike<TValue>(Expression<Func<T, TValue>> exp, TValue value, LikeEnum way)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> OrIn<TValue>(Expression<Func<T, TValue>> exp, IList<TValue> value)
        {
            throw new NotImplementedException();
        }


        public IOrder<T> Asc(params Expression<Func<T, object>>[] exps)
        {
            throw new NotImplementedException();
        }

        public IOrder<T> Desc(params Expression<Func<T, object>>[] exps)
        {
            throw new NotImplementedException();
        }

        public ISelect<T> Select(params Expression<Func<T, object>>[] exps)
        {
            throw new NotImplementedException();
        }

        public IJoin<T> Join<TJoin>(Expression<Func<T, object>> exp, Expression<Func<TJoin, object>> expJ, IWhere<TJoin> and = null)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMSORM<T> : ISelect<T>
    {
        //IMSORM<T> Select(params Expression<Func<T, object>>[] exps);


        //IMSORM<T> Join<TJoin>(TJoin join, Expression<Func<T, object>> exp, Expression<Func<TJoin, object>> expJ,);
        //IMSORM<T> Join<TJoin>(TJoin join, Expression<Func<T, object>> exp, Expression<Func<TJoin, object>> expJ, object );
        //IMSORM<T> LJoin<TLJoin>(Expression<Func<T, object>> exp);
        //IMSORM<T> RJoin<TRJoin>(Expression<Func<T, object>> exp);
        //IMSORM<T> FJoin<TFJoin>(Expression<Func<T, object>> exp);
    }

    public interface ISelect<T> : IJoin<T>
    {
        ISelect<T> Select(Expression<Func<T, object>>[] exps);
    }

    public interface IJoin<T> : IWhere<T>
    {

        IJoin<T> Join<TJoin>(Expression<Func<T, object>> exp, Expression<Func<TJoin, object>> expJ, IWhere<TJoin> and = null);
        //IJoin<T> LJoin<TLJoin>(Expression<Func<T, object>> exp);
        //IJoin<T> RJoin<TRJoin>(Expression<Func<T, object>> exp);
        //IJoin<T> FJoin<TFJoin>(Expression<Func<T, object>> exp);
    }

    public interface IWhere<T> : IOrder<T>
    {
        IWhere<T> Eq<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> Equal<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> Gt<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> GreatThan<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> Gte<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> GreatThanEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> Lt<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> LessThan<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> Lte<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> LessThanEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> Like<TValue>(Expression<Func<T, TValue>> exp, TValue value, LikeEnum way);
        IWhere<T> In<TValue>(Expression<Func<T, TValue>> exp, IList<TValue> value);

        IWhere<T> OrEq<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrGt<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrGreatThan<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrGte<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrGreatThanEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrLt<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrLessThan<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrLte<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrLessThanEqual<TValue>(Expression<Func<T, TValue>> exp, TValue value);
        IWhere<T> OrLike<TValue>(Expression<Func<T, TValue>> exp, TValue value, LikeEnum way);
        IWhere<T> OrIn<TValue>(Expression<Func<T, TValue>> exp, IList<TValue> value);


    }

    public interface IOrder<T>
    {
        IOrder<T> Asc(params Expression<Func<T, object>>[] exps);
        IOrder<T> Desc(params Expression<Func<T, object>>[] exps);
    }


    public class Test
    {
        public string Name { get; set; }

        public void Xxx()
        {
            new MSORM<Test>()
                .Select(x => x.Name)
                .Join<Test2>(x => x.Name, x => x.Name2)
                .Eq(x => x.Name, "11").OrLt(x => x.Name, "123")
                .Asc(x => x.Name, x => x.Name)
                .Desc(x => x.Name);
        }
    }

    public class Test2
    {
        public string Name2 { get; set; }
    }
}
