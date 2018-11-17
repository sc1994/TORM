using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IQueryOrder<T, TFunc> : IQuery<T>
    {
        IQueryOrder<T, TFunc> OrderA(params Expression<TFunc>[] exps);
        IQueryOrder<T, TFunc> OrderD(params Expression<TFunc>[] exps);
    }
}
