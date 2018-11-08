using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    public abstract class BaseExplain<T> : IExplain where T : Expression
    {
        public abstract void Explain(T exp, StringBuilder info);

        public void Explain(Expression exp, StringBuilder info)
        {
            Explain(exp as T, info);
        }
    }
}
