using System;
using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    public class ExplainBinary : BaseExplain<BinaryExpression>
    {
        public override void Explain(BinaryExpression exp, StringBuilder info)
        {
            if (exp.Left is MemberExpression member) // 树的左边直接解析
            {
                info.Appinfo(member.Member.Name);
            }
            else
            {
                throw new Exception("输入的表达式左边必须是字段");
            }

            info.Appinfo(exp.NodeType.ToString()); // 比较符
            ExplainTool.Explain(exp.Right, info); // 树的右边有多种情况
        }
    }
}
