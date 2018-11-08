using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Explain
{
    /// <summary>
    /// 表达式的成员
    /// </summary>
    public class ExplainMember : BaseExplain<MemberExpression>
    {
        public override void Explain(MemberExpression exp, StringBuilder info)
        {
            if (exp.Expression != null)
            {
                if (exp.Expression is ConstantExpression constant)
                {
                    // 如果是变量直接取值
                    info.Append(constant.Value.GetType().InvokeMember(exp.Member.Name, BindingFlags.GetField, null, constant.Value, null));
                }
                else
                {
                    info.Append(exp.Member.Name); // 如果树的右边也是个表达式 比如 Join 方法
                }
            }
            else
            {
                ExplainTool.Log("MemberExpression", $"{{\r\n  exp.Expression == null,  {exp.Member}\r\n}}");
                // 目前只有datetime.now之类的右边值会走到这，考虑标记为唯一差异化对待datetime
                info.Append(exp.Member);
            }
        }
    }
}
