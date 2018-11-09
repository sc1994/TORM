using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    public class ExplainLambda : BaseExplain<LambdaExpression>
    {
        public override void Explain(LambdaExpression exp, Content info)
        {
            ExplainTool.Explain(exp.Body, info);
        }
    }
}
