using System;
using System.Linq.Expressions;
using System.Text;

namespace Explain
{
    /// <summary>
    /// 函数，方法
    ///  1.表达式中调用了方法
    /// </summary>
    public class ExplainMethodCall : BaseExplain<MethodCallExpression>
    {
        public override void Explain(MethodCallExpression exp, StringBuilder info)
        {
            // 计算方法返回的结果
            info.Appinfo(Expression.Lambda(exp).Compile().DynamicInvoke());
        }
    }
}
