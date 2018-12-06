using System.Linq.Expressions;

namespace Explain
{
    public class ExplainBinary : BaseExplain<BinaryExpression>
    {
        public override void Explain(BinaryExpression exp, Content info)
        {
            ExplainTool.Explain(exp.Left, info);
            info.Append(exp.NodeType); // 比较符
            ExplainTool.Explain(exp.Right, info); // 树的右边有多种情况
        }
    }
}
