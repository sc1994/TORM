using System.Linq.Expressions;

namespace Explain
{
    public abstract class BaseExplain<T> : IExplain where T : Expression
    {
        public abstract void Explain(T exp);

        public void Explain(Expression exp)
        {
            Explain(exp as T);
        }
    }
}
