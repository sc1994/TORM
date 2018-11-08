using System.Linq.Expressions;

namespace Explain
{
    public interface IExplain
    {
        void Explain(Expression exp);
    }
}
