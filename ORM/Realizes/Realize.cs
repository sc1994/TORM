using ORM.Interface;
using System;
using System.Linq.Expressions;
using ORM.Interface.IQuery;

namespace ORM.Realizes
{
    public class Relize<T, TFunc, TFuncBool> : RealizeQuery<T>, IQuerySelect<T, TFunc, TFuncBool>
    {
        public IQueryWhere<T, TFunc, TFuncBool> Where(TFuncBool exp)
        {
            throw new NotImplementedException();
        }

        public IQueryOrder<T, TFunc> OrderA(TFunc exp)
        {
            throw new NotImplementedException();
        }

        public IQueryOrder<T, TFunc> OrderD(TFunc exp)
        {
            throw new NotImplementedException();
        }

        public IQueryOrder<T, TFunc> OrderA(params TFunc[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryOrder<T, TFunc> OrderD(params TFunc[] exps)
        {
            throw new NotImplementedException();
        }

        public IQueryHaving<T, TFunc, TFuncBool> Having(TFuncBool exp)
        {
            throw new NotImplementedException();
        }

        public IQueryGroup<T, TFunc, TFuncBool> Group(TFunc exp)
        {
            throw new NotImplementedException();
        }

        public IQueryGroup<T, TFunc, TFuncBool> Group(params TFunc[] exps)
        {
            throw new NotImplementedException();
        }

        public IQuerySelect<T, TFunc, TFuncBool> Select(Expression<TFunc> exp)
        {
            throw new NotImplementedException();
        }

        public IQuerySelect<T, TFunc, TFuncBool> Select(params Expression<TFunc>[] exps)
        {
            throw new NotImplementedException();
        }

        public IQuerySelect<T, TFunc, TFuncBool> Select(Expression<TFunc> exp, string alias)
        {
            throw new NotImplementedException();
        }

        public IQuerySelect<T, TFunc, TFuncBool> Select(params (Expression<TFunc> exp, string alias)[] exps)
        {
            throw new NotImplementedException();
        }
    }

    public class Realize<T> : Relize<T, Func<T, object>, Func<T, bool>>
    {

    }

    public class Realize<T, TJoin> : Relize<T, Func<T, TJoin, object>, Func<T, TJoin, bool>>
    {

    }
}
