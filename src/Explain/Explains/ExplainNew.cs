using System.Linq.Expressions;

namespace Explain
{
    public class ExplainNew : BaseExplain<NewExpression>
    {
        public override void Explain(NewExpression exp, Content info)
        {
            throw new System.NotImplementedException(); // todo 目前还不能解决获取NewExpression的结果
        }
    }
}
