using System.Linq.Expressions;

namespace ORM.Interface
{
    public interface IWhere { }

    public interface IWhere<TFunc, out TResult> where TResult : IWhere
    {
        TResult Where(params Expression<TFunc>[] exps);
    }
}
