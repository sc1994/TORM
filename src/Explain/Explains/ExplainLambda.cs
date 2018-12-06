using System.Linq.Expressions;

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
