using ORM.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ORM.Realizes
{
    public class Realize<T> : RealizeQuery<T>, ISelect<T>
    {
        public IOrder<T> Group(Expression<Func<T, object[]>> exp)
        {
            _groups.Add(exp);
            return this;
        }

        public IOrder<T> Group(params Expression<Func<T, object>>[] exps)
        {
            _groups.AddRange(exps);
            return this;
        }

        public IOrder<T> OrderA(Expression<Func<T, object[]>> exp)
        {
            _orderAs.Add(exp);
            return this;
        }

        public IOrder<T> OrderD(Expression<Func<T, object[]>> exp)
        {
            _orderDs.Add(exp);
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
            _where.Add(exp);
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
            foreach (var item in exps)
            {
                _selectAlias.Add((item.exp, item.alias));
            }
            return this;
        }

        public ISelect<T> Select(Expression<Func<T, object[]>> exp)
        {
            _selects.Add(exp);
            return this;
        }
    }

    public class Realize<T, TJoin> : RealizeQuery<T>, ISelect<T, TJoin>
    {
        public IOrder<T, TJoin> Group(Expression<Func<T, TJoin, object[]>> exp)
        {
            _groups.Add(exp);
            return this;
        }

        public IOrder<T, TJoin> Group(params Expression<Func<T, TJoin, object>>[] exps)
        {
            _groups.AddRange(exps);
            return this;
        }

        public IOrder<T, TJoin> OrderA(Expression<Func<T, TJoin, object[]>> exp)
        {
            _orderAs.Add(exp);
            return this;
        }

        public IOrder<T, TJoin> OrderD(Expression<Func<T, TJoin, object[]>> exp)
        {
            _orderDs.Add(exp);
            return this;
        }

        public IOrder<T, TJoin> OrderA(params Expression<Func<T, TJoin, object>>[] exps)
        {
            _orderAs.AddRange(exps);
            return this;
        }

        public IOrder<T, TJoin> OrderD(params Expression<Func<T, TJoin, object>>[] exps)
        {
            _orderDs.AddRange(exps);
            return this;
        }

        public IWhere<T, TJoin> Where(Expression<Func<T, TJoin, bool>> exp)
        {
            _where.Add(exp);
            return this;
        }

        public IJoin<T, TJoin> Join(Expression<Func<T, TJoin, bool>> exp)
        {
            _join.Add((exp, JoinEnum.Join));
            return this;
        }

        public IJoin<T, TJoin> JoinL(Expression<Func<T, TJoin, bool>> exp)
        {
            _join.Add((exp, JoinEnum.LeftJoin));
            return this;
        }

        public IJoin<T, TJoin> JoinR(Expression<Func<T, TJoin, bool>> exp)
        {
            _join.Add((exp, JoinEnum.RightJoin));
            return this;
        }

        public IJoin<T, TJoin> JoinF(Expression<Func<T, TJoin, bool>> exp)
        {
            _join.Add((exp, JoinEnum.FullJoin));
            return this;
        }

        public ISelect<T, TJoin> Select(params Expression<Func<T, TJoin, object>>[] exps)
        {
            _selects.AddRange(exps);
            return this;
        }

        public ISelect<T, TJoin> Select(Expression<Func<T, TJoin, object>> exp, string alias)
        {
            _selectAlias.Add((exp, alias));
            return this;
        }

        public ISelect<T, TJoin> Select(params (Expression<Func<T, TJoin, object>> exp, string alias)[] exps)
        {
            foreach (var item in exps)
            {
                _selectAlias.Add((item.exp, item.alias));
            }
            return this;
        }

        public ISelect<T, TJoin> Select(Expression<Func<T, TJoin, object[]>> exp)
        {
            throw new NotImplementedException();
        }
    }
}
