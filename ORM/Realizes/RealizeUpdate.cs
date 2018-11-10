using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ORM.Interface;

namespace ORM.Realizes
{
    public class RealizeUpdate<T> : IUpdateSet<T>
    {
        public int Update(int top = 0)
        {
            throw new NotImplementedException();
        }

        public List<UpdateRecord> Update(int top = 0, bool record = false)
        {
            throw new NotImplementedException();
        }

        public IWhere<T> Where(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public ISet<T> Set<TValue>(Expression<Func<T, TValue>> exp, string value)
        {
            throw new NotImplementedException();
        }

        public ISet<T> Set<TValue>(params (Expression<Func<T, object>> exp, string value)[] exps)
        {
            throw new NotImplementedException();
        }
    }
}
