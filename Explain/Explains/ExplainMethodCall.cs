using System.Linq.Expressions;

namespace Explain
{
    /// <summary>
    /// 函数，方法
    ///  1.表达式中调用了方法
    /// </summary>
    public class ExplainMethodCall : BaseExplain<MethodCallExpression>
    {
        public override void Explain(MethodCallExpression exp, Content info)
        {
            if (exp.Method.DeclaringType != null && exp.Method.DeclaringType.FullName == "ORM.ORMTool")
            {
                ExplainTool.Explain(exp.Arguments[0], info);
                info.Append(exp.Method);
                if (exp.Arguments.Count > 1)
                    ExplainTool.Explain(exp.Arguments[1], info);
            }
            else
            {
                info.Append(Expression.Lambda(exp).Compile().DynamicInvoke());
            }
        }
    }
}
