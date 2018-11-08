using System.Linq.Expressions;

namespace Explain
{
    public class ExplainLambda : BaseExplain<LambdaExpression>
    {
        public override void Explain(LambdaExpression exp)
        {
            ExplainTool.Explain(exp.Body);
        }
    }
}
