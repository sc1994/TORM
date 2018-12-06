using System.Linq.Expressions;

namespace Explain
{
    public class ExplainParameter : BaseExplain<ParameterExpression>
    {
        public override void Explain(ParameterExpression exp, Content info)
        {
            info.Append(exp.Type);
        }
    }
}
