using System;
using System.Linq.Expressions;

namespace Demo
{
    /// <summary>
    /// 存放未实现的
    /// </summary>
    public partial class Visitor
    {
        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.BlockExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitBlock(BlockExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.CatchBlock"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.ConditionalExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问当前内容 <see cref="T:System.Linq.Expressions.DebugInfoExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问当前内容 <see cref="T:System.Linq.Expressions.DefaultExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitDefault(DefaultExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.DynamicExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitDynamic(DynamicExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.ElementInit"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： extension expression.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitExtension(Expression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.GotoExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitGoto(GotoExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.IndexExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitIndex(IndexExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.InvocationExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.LabelExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitLabel(LabelExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问当前内容 <see cref="T:System.Linq.Expressions.LabelTarget"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.ListInitExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitListInit(ListInitExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.LoopExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitLoop(LoopExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.MemberAssignment"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.MemberBinding"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.MemberInitExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.MemberListBinding"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.MemberMemberBinding"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.NewExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitNew(NewExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.NewArrayExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.SwitchExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.SwitchCase"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.TryExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitTry(TryExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.TypeBinaryExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.UnaryExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            Log(node.ToString());
            throw new NotImplementedException();
        }
    }
}
