using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    public interface IExplain
    {
        void Explain(Expression exp, StringBuilder info);
    }
}
