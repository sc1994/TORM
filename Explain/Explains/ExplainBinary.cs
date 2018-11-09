using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    public class ExplainBinary : BaseExplain<BinaryExpression>
    {
        public override void Explain(BinaryExpression exp, StringBuilder info)
        {
            if (ExistsBracket(exp.Left))
            {
                info.Appinfo("(");
                ExplainTool.Explain(exp.Left, info);
                info.Appinfo(")");
            }
            else
            {
                ExplainTool.Explain(exp.Left, info);
            }

            info.Appinfo(exp.NodeType.ToString()); // 比较符
            if (ExistsBracket(exp.Left))
            {
                info.Appinfo("(");
                ExplainTool.Explain(exp.Right, info); // 树的右边有多种情况
                info.Appinfo(")");
            }
            else
            {
                ExplainTool.Explain(exp.Right, info); // 树的右边有多种情况
            }


        }

        /// <summary>
        /// 存在括号
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        private static bool ExistsBracket(Expression expr)
        {
            var s = expr.ToString();
            return s != null && s.Length > 5 && s[0] == '(' && s[1] == '(';
        }
    }
}
