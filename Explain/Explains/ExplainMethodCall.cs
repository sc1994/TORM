using System;
using System.Linq.Expressions;

namespace Explain
{
    /// <summary>
    /// 函数，方法
    ///  1.表达式中调用了方法
    /// </summary>
    public class ExplainMethodCall : BaseExplain<MethodCallExpression>
    {
        public override void Explain(MethodCallExpression exp)
        {
            // 计算方法返回的结果
            Console.WriteLine(Expression.Lambda(exp).Compile().DynamicInvoke());
        }
    }
}
