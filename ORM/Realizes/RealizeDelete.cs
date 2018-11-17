using System;
using System.Linq.Expressions;
using ORM.Interface.IDelete;

namespace ORM.Realizes
{
    public class RealizeDelete<T> : RealizeToSql<T>, IDeleteWhere<T>
    {
        public int Delete()
        {

            throw new System.NotImplementedException();
        }

        public int Delete(int top)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(T model)
        {
            throw new System.NotImplementedException();
        }

        public IDeleteWhere<T> Where(params Expression<Func<T, bool>>[] exps)
        {
            _where.AddRange(exps);
            return this;
        }
    }
}
