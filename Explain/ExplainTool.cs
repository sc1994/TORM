using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Explain
{
    public class ExplainTool
    {
        /// <summary>
        /// 目前支持解析的表达式类型，作用是为了能取出对应的自定义表达式扩展实例
        /// </summary>
        private static readonly string[] Expressions =
        {
            "LambdaExpression",
            "BinaryExpression",
            "MemberExpression",
            "ConstantExpression",
            "MethodCallExpression",
            "UnaryExpression",
            "NewArrayExpression",
            "ListInitExpression",
            "ParameterExpression",
            "NewExpression"
        };

        public static string[] Methods =
        {
            "In",
            "NotIn",
            "Contains",
            "StartsWith",
            "EndsWith",
            "Count",
            "Max",
            "Min"
        };

        public static string[] MethodLikes =
        {
            "Contains",
            "StartsWith",
            "EndsWith",
        };

        /// <summary>
        /// 表达式的扩展实例
        /// </summary>
        private static readonly Dictionary<string, IExplain> Ports = InitPorts();

        /// <summary>
        /// 解释器
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="info"></param>
        public static void Explain(Expression exp, Content info)
        {
            if (exp != null)
            {
                GetPort(exp).Explain(exp, info);
            }
        }

        public static void Log(string module, string info)
        {
            File.AppendAllLines("d:/1.txt", new[] { $"\r\n\r\n------{DateTime.Now:s}--{module}------\r\ninfo：{info}\r\n------{DateTime.Now:s}--{module}------\r\n" });
        }

        /// <summary>
        /// 初始化接口实例
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, IExplain> InitPorts()
        {
            var ports = new Dictionary<string, IExplain>();
            var nameSpace = typeof(ExplainTool).Namespace; // 获取当前的命名空间
            foreach (var exp in Expressions)
            {
                // 获取表达式的实例（固定格式）
                var type = Type.GetType($"{nameSpace}.Explain{exp.Replace("Expression", "")}");
                // 将实例存储起来
                ports.Add(exp, (IExplain)Activator.CreateInstance(type ?? throw new InvalidOperationException()));
            }
            return ports;
        }

        /// <summary>
        /// 获取一个对应类型的表达式实例
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static IExplain GetPort(Expression exp)
        {
            var type = SwitchExpression(exp); // 筛选对应的表达式类型

            return Ports[type]; // 依据表达式类型获取表达式扩展的实例
        }

        /// <summary>
        /// 获取表达的类型描述
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static string SwitchExpression(Expression exp)
        {
            switch (exp)
            {
                case BinaryExpression _:
                    return "BinaryExpression";
                case BlockExpression _:
                    return "BlockExpression";
                case ConditionalExpression _:
                    return "ConditionalExpression";
                case ConstantExpression _:
                    return "ConstantExpression";
                case DebugInfoExpression _:
                    return "DebugInfoExpression";
                case DefaultExpression _:
                    return "DefaultExpression";
                case DynamicExpression _:
                    return "DynamicExpression";
                case GotoExpression _:
                    return "GotoExpression";
                case IndexExpression _:
                    return "IndexExpression";
                case InvocationExpression _:
                    return "InvocationExpression";
                case LabelExpression _:
                    return "LabelExpression";
                case LambdaExpression _:
                    return "LambdaExpression";
                case ListInitExpression _:
                    return "ListInitExpression";
                case LoopExpression _:
                    return "LoopExpression";
                case MemberExpression _:
                    return "MemberExpression";
                case MemberInitExpression _:
                    return "MemberInitExpression";
                case MethodCallExpression _:
                    return "MethodCallExpression";
                case NewArrayExpression _:
                    return "NewArrayExpression";
                case NewExpression _:
                    return "NewExpression";
                case ParameterExpression _:
                    return "ParameterExpression";
                case RuntimeVariablesExpression _:
                    return "RuntimeVariablesExpression";
                case SwitchExpression _:
                    return "SwitchExpression";
                case TryExpression _:
                    return "TryExpression";
                case TypeBinaryExpression _:
                    return "TypeBinaryExpression";
                case UnaryExpression _:
                    return "UnaryExpression";
                default:
                    throw new Exception(nameof(exp));
            }
        }
    }


}
