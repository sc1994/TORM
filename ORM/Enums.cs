using System;
using System.Linq.Expressions;

namespace ORM
{
    public enum LikeEnum
    {
        Right,
        Left,
        Full
    }

    public class Const
    {
        public static string[] Methods = new[]
        {
            "In",
            "NotIn",
            "LikeF",
            "LikeR",
            "LikeL",
            "Count",
            "Max",
            "Min"
        };
    }

    static class EnumHelper
    {
        public static string ToString(this LikeEnum that)
        {
            //switch (that)
            //{
            //      LikeEnum.Full  
            //}
            return ""; // todo
        }

        public static string ToExplain(this ExpressionType? that)
        {
            switch (that)
            {
                case null:
                    return null;
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.AndAlso:
                    return "AND";
                case ExpressionType.OrElse:
                    return "OR";
                default: throw new Exception("错误的ExpressionType：" + nameof(that));
            }
        }
    }
}
