using System;
using System.Linq.Expressions;

namespace Explain
{
    public class ExplainBinary : BaseExplain<BinaryExpression>
    {
        public override void Explain(BinaryExpression exp)
        {
            Console.WriteLine((exp.Left as MemberExpression).Member.Name); // 树的左边直接解析
            Console.WriteLine(exp.NodeType.ToString()); // 比较符
            ExplainTool.Explain(exp.Right); // 树的右边有多种情况
        }
    }
}
