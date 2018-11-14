using ORM.Interface;
using System;
using System.Linq.Expressions;

namespace ORM.Realizes
{
    public class Realize<T> : RealizeQuery<T>, IQuerySelect<T>
    {
        public IQueryOrder<T> Group(Expression<Func<T, object[]>> exp)
        {
            _groups.Add(exp);
            return this;
        }

        public IQueryOrder<T> Group(params Expression<Func<T, object>>[] exps)
        {
            _groups.AddRange(exps);
            return this;
        }

        public IQueryOrder<T> OrderA(Expression<Func<T, object[]>> exp)
        {
            _orders.Add((exp, OrderEnum.Asc));
            return this;
        }

        public IQueryOrder<T> OrderD(Expression<Func<T, object[]>> exp)
        {
            _orders.Add((exp, OrderEnum.Desc));
            return this;
        }

        public IQueryOrder<T> OrderA(params Expression<Func<T, object>>[] exps)
        {
            foreach (var item in exps)
            {
                _orders.Add((item, OrderEnum.Asc));
            }
            return this;
        }

        public IQueryOrder<T> OrderD(params Expression<Func<T, object>>[] exps)
        {
            foreach (var item in exps)
            {
                _orders.Add((item, OrderEnum.Desc));
            }
            return this;
        }

        public IQuerySelect<T> Select(params Expression<Func<T, object>>[] exps)
        {
            _selects.AddRange(exps);
            return this;
        }

        public IQuerySelect<T> Select(Expression<Func<T, object>> exp, string alias)
        {
            _selectAlias.Add((exp, alias));
            return this;
        }

        public IQuerySelect<T> Select(params (Expression<Func<T, object>> exp, string alias)[] exps)
        {
            foreach (var item in exps)
            {
                _selectAlias.Add((item.exp, item.alias));
            }
            return this;
        }

        public IQuerySelect<T> Select(Expression<Func<T, object[]>> exp)
        {
            _selects.Add(exp);
            return this;
        }

        public IQueryWhere<T> Where(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }
    }

    public class Realize<T, TJoin> : RealizeQuery<T>, IQuerySelect<T, TJoin>
    {
        public IQueryOrder<T, TJoin> Group(Expression<Func<T, TJoin, object[]>> exp)
        {
            _groups.Add(exp);
            return this;
        }

        public IQueryOrder<T, TJoin> Group(params Expression<Func<T, TJoin, object>>[] exps)
        {
            _groups.AddRange(exps);
            return this;
        }

        public IQueryOrder<T, TJoin> OrderA(Expression<Func<T, TJoin, object[]>> exp)
        {
            _orders.Add((exp, OrderEnum.Asc));
            return this;
        }

        public IQueryOrder<T, TJoin> OrderD(Expression<Func<T, TJoin, object[]>> exp)
        {
            _orders.Add((exp, OrderEnum.Desc));
            return this;
        }

        public IQueryOrder<T, TJoin> OrderA(params Expression<Func<T, TJoin, object>>[] exps)
        {
            foreach (var item in exps)
            {
                _orders.Add((item, OrderEnum.Asc));
            }
            return this;
        }

        public IQueryOrder<T, TJoin> OrderD(params Expression<Func<T, TJoin, object>>[] exps)
        {
            foreach (var item in exps)
            {
                _orders.Add((item, OrderEnum.Desc));
            }
            return this;
        }

        public IQueryJoin<T, TJoin> Join(Expression<Func<T, TJoin, bool>> exp)
        {
            _join.Add((exp, JoinEnum.Join));
            return this;
        }

        public IQueryJoin<T, TJoin> JoinL(Expression<Func<T, TJoin, bool>> exp)
        {
            _join.Add((exp, JoinEnum.LeftJoin));
            return this;
        }

        public IQueryJoin<T, TJoin> JoinR(Expression<Func<T, TJoin, bool>> exp)
        {
            _join.Add((exp, JoinEnum.RightJoin));
            return this;
        }

        public IQueryJoin<T, TJoin> JoinF(Expression<Func<T, TJoin, bool>> exp)
        {
            _join.Add((exp, JoinEnum.FullJoin));
            return this;
        }

        public IQuerySelect<T, TJoin> Select(params Expression<Func<T, TJoin, object>>[] exps)
        {
            _selects.AddRange(exps);
            return this;
        }

        public IQuerySelect<T, TJoin> Select(Expression<Func<T, TJoin, object>> exp, string alias)
        {
            _selectAlias.Add((exp, alias));
            return this;
        }

        public IQuerySelect<T, TJoin> Select(params (Expression<Func<T, TJoin, object>> exp, string alias)[] exps)
        {
            foreach (var item in exps)
            {
                _selectAlias.Add((item.exp, item.alias));
            }
            return this;
        }

        public IQuerySelect<T, TJoin> Select(Expression<Func<T, TJoin, object[]>> exp)
        {
            throw new NotImplementedException();
        }

        IQueryWhere<T, TJoin> IWhere<T, TJoin, IQueryWhere<T, TJoin>>.Where(Expression<Func<T, TJoin, bool>> exp)
        {
            throw new NotImplementedException();
        }
    }
}
