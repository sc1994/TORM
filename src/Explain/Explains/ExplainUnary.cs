using System.Linq.Expressions;

namespace Explain
{
    /// <summary>
    /// 一元表达式
    ///  1.强制隐式类型转换
    /// </summary>
    public class ExplainUnary : BaseExplain<UnaryExpression>
    {
        public override void Explain(UnaryExpression exp, Content info)
        {
            ExplainTool.Explain(exp.Operand, info);
        }
    }
}
