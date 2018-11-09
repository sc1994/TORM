using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    public class ExplainBinary : BaseExplain<BinaryExpression>
    {
        public override void Explain(BinaryExpression exp, Content info)
        {
            if (ExistsBracket(exp.Left) || ExistsBracket(exp.Right))
            {
                throw new System.Exception("表达式中不能支持括号的表示方式，如果需要 (a=1 or b=2) 的代码方式请使用 or 的 api。");
            }
            ExplainTool.Explain(exp.Left, info);
            info.Append(exp.NodeType); // 比较符
            ExplainTool.Explain(exp.Right, info); // 树的右边有多种情况
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
