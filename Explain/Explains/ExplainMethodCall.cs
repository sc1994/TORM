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
            if (exp.Method.DeclaringType != null && exp.Method.DeclaringType.FullName == "ORM.ORMTool")
            {
                foreach (var item in exp.Arguments)
                {
                    ExplainTool.Explain(item, info);
                }
                info.Appinfo(exp.Method.Name);
                // todo 

            }
            else
            {
                info.Appinfo(Expression.Lambda(exp).Compile().DynamicInvoke());
            }
        }
    }
}
