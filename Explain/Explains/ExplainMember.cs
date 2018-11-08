using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Explain
{
    /// <summary>
    /// 表达式的成员
    /// </summary>
    public class ExplainMember : BaseExplain<MemberExpression>
    {
        public override void Explain(MemberExpression exp)
        {
            if (exp.Expression != null)
            {
                if (exp.Expression is ConstantExpression constant)
                {
                    // 如果是变量直接取值
                    Console.WriteLine(constant.Value.GetType().InvokeMember(exp.Member.Name, BindingFlags.GetField, null, constant.Value, null));
                }
                else
                {
                    Console.WriteLine(exp.Member.Name); // 如果树的右边也是个表达式 比如 Join 方法
                }
            }
            else
            {
                Console.WriteLine(exp.Member.Name); // 如果树的右边也是个表达式
            }
        }
    }
}
