using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace Demo
{
    public partial class Visitor : ExpressionVisitor
    {
        /// <summary>
        /// 是否读取成员信息
        /// </summary>
        private bool isReadMember = true;
        private readonly HashSet<Type> _useModels = new HashSet<Type>();
        /// <summary>
        /// 操作符比较
        /// </summary>
        private readonly IComparer<ExpressionType> _comparer = new OperatorPrecedenceComparer();

        /// <summary>
        /// 参数
        /// </summary>
        private readonly Dictionary<string, object> _params = new Dictionary<string, object>();
        /// <summary>
        /// sql
        /// </summary>
        private string _sql = string.Empty;

        /// <summary>
        /// 操作符
        /// </summary>
        private static readonly Dictionary<ExpressionType, string> _operator = new Dictionary<ExpressionType, string>
        {
            { ExpressionType.Equal , " = " },
            { ExpressionType.And , "\r\nAND   " },
            { ExpressionType.AndAlso , "\r\nAND   " },
            { ExpressionType.Or , "\r\nOR    " },
            { ExpressionType.OrElse , "\r\nOR    " },
            { ExpressionType.NotEqual , " != " },
            { ExpressionType.GreaterThan , " > " },
            { ExpressionType.GreaterThanOrEqual , " >= " },
            { ExpressionType.LessThan , " < " },
            { ExpressionType.LessThanOrEqual , " <= " }
        };
        /// <summary>
        /// string 支持的方法
        /// </summary>
        private static readonly Dictionary<string, string> _stringMethod = new Dictionary<string, string>
        {
            { "Equals" , "== {0}" },
            { "StartsWith" , "LIKE '%'+{0}" },
            { "EndsWith" , "LIKE {0}+'%'" },
            { "Contains" , "LIKE '%'+{0}+'%'" }
        };
        /// <summary>
        /// 数组支持的方法
        /// </summary>
        private static readonly Dictionary<string, string> _arrayMethod = new Dictionary<string, string>
        {
            { "Contains" , " IN ({0})" },
        };
        private static readonly Dictionary<string, string> _dateMethod = new Dictionary<string, string>
                                                                        {
                                                                            { "AddDays", "DATE_ADD({0},INTERVAL {1} DAY)" }
                                                                        };
        /// <summary>
        /// 负责将不同的类型分发到不同的字典中
        /// </summary>
        private static readonly Dictionary<Type, Dictionary<string, string>> _typeMethod = new Dictionary<Type, Dictionary<string, string>>
        {
            {typeof(string), _stringMethod },
            {typeof(DateTime), _dateMethod },
        };

        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="node"></param>
        public (string sql, Dictionary<string, object> param) Translate(Expression node)
        {
            Visit(node);
            return (_sql, _params);
        }

        private void Log(string content)
        {
            var st = new System.Diagnostics.StackTrace();
            var sfs = st.GetFrames();
            var filterdName = "ResponseWrite,ResponseWriteError,";
            var fullName = string.Empty;
            for (var i = 1; i < sfs.Length; ++i)
            {
                if (System.Diagnostics.StackFrame.OFFSET_UNKNOWN == sfs[i].GetILOffset()) break;
                var methodName = sfs[i].GetMethod().Name;
                if (filterdName.Contains(methodName)) continue;
                fullName = methodName + "()->" + fullName;
            }

            fullName = fullName.TrimEnd('-', '>');
            File.AppendAllText("D:/1.log", $"\r\n\r\n=========================\r\n{fullName}\r\n{content}\r\n=========================");
        }

        private bool IsBaseType(object value)
        {
            switch (value)
            {
                case short _:
                case int _:
                case long _:
                case string _:
                case double _:
                case float _:
                case bool _:
                case byte _:
                case char _:
                case decimal _:
                    return true;
                default:
                    return false;
            } // todo 类型补全
        }
    }

    static class StringExtend
    {
        public static string Append(this string that, string s)
        {
            if (that.Contains("{0}"))
            {
                that = that.Replace("{0}", s);
            }
            else
            {
                that += s;
            }
            return that;
        }
    }

    /// <summary>
    /// 重写比较方法
    /// </summary>
    class OperatorPrecedenceComparer : Comparer<ExpressionType>
    {
        public override int Compare(ExpressionType x, ExpressionType y)
            => Precedence(x).CompareTo(Precedence(y));

        private int Precedence(ExpressionType expressionType)
        {
            if (expressionType == ExpressionType.OrElse)
                return expressionType.GetHashCode();
            return -100;
        }
    }
}


