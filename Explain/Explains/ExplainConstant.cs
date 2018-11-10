using System.Linq.Expressions;

namespace Explain
{
    /// <summary>
    /// 常量
    ///  1.表达式中含有常量
    /// </summary>
    public class ExplainConstant : BaseExplain<ConstantExpression>
    {
        public override void Explain(ConstantExpression exp, Content info)
        {
            info.Append(exp.Value);
        }
    }
}
