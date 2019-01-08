using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Demo
{
    /// <summary>
    /// 已实现的
    /// </summary>
    public partial class Visitor
    {
        /// <summary>
        /// 基础方法，负责将不同类型的表达式发送到对应的方法中
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        private void Visit(Expression parent, Expression child)
        {
            if (_comparer.Compare(child.NodeType, parent.NodeType) > 0)
            {
                _sql = _sql.Append("(");
                base.Visit(child);
                _sql = _sql.Append(")");
            }
            else
            {
                base.Visit(child);
            }
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.BinaryExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Log(node.ToString());
            Visit(node, node.Left);
            if (_operator.TryGetValue(node.NodeType, out string o))
            {
                _sql = _sql.Append(o);
            }
            else
            {
                throw new NotImplementedException();// todo 错误收集
            }

            Visit(node, node.Right);
            return node;
        }

        /// <summary>访问当前内容 <see cref="T:System.Linq.Expressions.ConstantExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            Log(node.ToString());
            object value;
            if (IsBaseType(node.Value)) // 常量类型
            {
                value = node.Value;
            }
            else
            {
                value = node.Type.GetFields()[0].GetValue(node.Value);
            }
            var p = "@param" + _params.Count;
            _params.Add(p, value);
            _sql = _sql.Append(p);
            return node;
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.MemberExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            Log(node.ToString());
            if (node.Member.ReflectedType?.FullName == "System.DateTime")
            {
                var p = $"@param{_params.Count}";
                _sql = _sql.Append(p);
                switch (node.Member.Name)
                {
                    // 时间特殊处理
                    case "Now":
                        _params.Add(p, DateTime.Now);
                        break;
                    case "Today":
                        _params.Add(p, DateTime.Today);
                        break;
                    default:
                        throw new NotImplementedException();// todo 错误收集
                }
            }
            else
            {
                Visit(node.Expression);
            }
            if (isReadMember)
            {
                _sql = _sql.Append("." + node.Member.Name);
                isReadMember = false;
            }
            return node;
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.MethodCallExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Log(node.ToString());

            if (node.Object == null)
            {
                return node; // todo null的特殊处理
            }

            if (!node.Object.ToString().StartsWith("value("))// 很low的判断
            {
                // 对模型字段直接进行方法操作，需将方法转译成t-sql语法
                if (_typeMethod.TryGetValue(node.Object.Type, out var methods))
                {
                    if (methods.TryGetValue(node.Method.Name, out var m))
                    {
                        _sql = _sql.Append(m);
                    }
                    else
                    {
                        throw new NotImplementedException(); // 需要适配
                    }
                }
                else
                {
                    throw new NotImplementedException(); // 需要适配
                }
            }
            else if (node.Object.Type.IsArray ||
                 node.Object.Type.IsGenericType) // array 类型的特殊处理
            {
                if (_arrayMethod.TryGetValue(node.Method.Name, out var m))
                {
                    _sql = _sql.Append(m);
                }
            }
            else // 其他类型在这里处理方法转义。
            {
                if (_typeMethod.TryGetValue(node.Object.Type, out var methods))
                {
                    if (methods.TryGetValue(node.Method.Name, out var m))
                    {
                        _sql = _sql.Append(m);
                    }
                }
            }
            Visit(node.Object);
            //Visit(node.Arguments); // 解析方法传入的变量
            return node;
        }

        /// <summary>访问子内容为： <see cref="T:System.Linq.Expressions.Expression`1"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <typeparam name="T">委托类型</typeparam>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Log(node.ToString());
            return base.VisitLambda(node);
        }

        /// <summary>访问当前内容 <see cref="T:System.Linq.Expressions.ParameterExpression"></see>.</summary>
        /// <param name="node">被访问的表达式</param>
        /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            Log(node.ToString());
            _sql = _sql.Append(node.Type.Name);
            _useModels.Add(node.Type); // 添加已使用的数据模型
            isReadMember = true;
            return node;
        }
    }
}
