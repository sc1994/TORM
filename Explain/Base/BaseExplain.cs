using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    public abstract class BaseExplain<T> : IExplain where T : Expression
    {
        public abstract void Explain(T exp, Content info);

        public void Explain(Expression exp, Content info)
        {
            Explain(exp as T, info);
        }
    }
}
