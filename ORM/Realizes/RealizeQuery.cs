using ORM.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ORM.Realizes
{
    public class RealizeQuery<T> : IQuery<T>
    {
        protected List<Expression<Func<T, bool>>> _ands = new List<Expression<Func<T, bool>>>();
        protected List<Expression<Func<T, bool>>> _ors = new List<Expression<Func<T, bool>>>();
        protected List<Expression<Func<T, object[]>>> _selectss = new List<Expression<Func<T, object[]>>>();
        protected List<Expression<Func<T, object>>> _selects = new List<Expression<Func<T, object>>>();
        protected List<(Expression<Func<T, object>>, string)> _selectAlias = new List<(Expression<Func<T, object>>, string)>();
        protected List<Expression<Func<T, object[]>>> _orderDss = new List<Expression<Func<T, object[]>>>();
        protected List<Expression<Func<T, object>>> _orderDs = new List<Expression<Func<T, object>>>();
        protected List<Expression<Func<T, object[]>>> _orderAss = new List<Expression<Func<T, object[]>>>();
        protected List<Expression<Func<T, object>>> _orderAs = new List<Expression<Func<T, object>>>();
        protected List<Expression<Func<T, object[]>>> _groupss = new List<Expression<Func<T, object[]>>>();
        protected List<Expression<Func<T, object>>> _groups = new List<Expression<Func<T, object>>>();

        public bool Exist()
        {
            throw new NotImplementedException();
        }

        public T First()
        {
            throw new NotImplementedException();
        }

        public TOther First<TOther>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TOther> Find<TOther>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find()
        {
            throw new NotImplementedException();
        }

        public (IEnumerable<T> data, int total) Page(int index, int size)
        {
            throw new NotImplementedException();
        }

        public (IEnumerable<TOther> data, int total) Page<TOther>(int index, int size)
        {
            throw new NotImplementedException();
        }


        private StringBuilder GetWhere()
        {
            
        }
    }
}
