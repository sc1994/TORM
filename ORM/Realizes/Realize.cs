using ORM.Interface;
using System;
using System.Linq.Expressions;

namespace ORM.Realizes
{
    public class Realize<T> : RealizeQuery<T>, ISelect<T>
    {
        public IOrder<T> Group(Expression<Func<T, object[]>> exp)
        {
            _groupss.Add(exp);
            return this;
        }

        public IOrder<T> Group(params Expression<Func<T, object>>[] exps)
        {
            _groups.AddRange(exps);
            return this;
        }

        public IOrder<T> OrderA(Expression<Func<T, object[]>> exp)
        {
            _orderAss.Add(exp);
            return this;
        }

        public IOrder<T> OrderD(Expression<Func<T, object[]>> exp)
        {
            _orderDss.Add(exp);
            return this;
        }

        public IOrder<T> OrderA(params Expression<Func<T, object>>[] exps)
        {
            _orderAs.AddRange(exps);
            return this;
        }

        public IOrder<T> OrderD(params Expression<Func<T, object>>[] exps)
        {
            _orderDs.AddRange(exps);
            return this;
        }

        public IWhere<T> Where(Expression<Func<T, bool>> exp)
        {
            _ands.Add(exp);
            return this;
        }

        public ISelect<T> Select(params Expression<Func<T, object>>[] exps)
        {
            _selects.AddRange(exps);
            return this;
        }

        public ISelect<T> Select(Expression<Func<T, object>> exp, string alias)
        {
            _selectAlias.Add((exp, alias));
            return this;
        }

        public ISelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps)
        {
            _selectAlias.AddRange(exps);
            return this;
        }


        public ISelect<T> Select(Expression<Func<T, object[]>> exp)
        {
            _selectss.Add(exp);
            return this;
        }
    }


}
