using System;
using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    /// <summary>
    /// 常量
    ///  1.表达式中含有常量
    /// </summary>
    public class ExplainConstant : BaseExplain<ConstantExpression>
    {
        public override void Explain(ConstantExpression exp, StringBuilder info)
        {
            info.Append(exp.Value);
        }
    }
}
