using System;
using System.Linq.Expressions;

namespace ORM
{
    public enum JoinEnum
    {
        LeftJoin,
        RightJoin,
        Join,
        FullJoin
    }

    static class EnumHelper
    {
        public static string ToExplain(this JoinEnum that)
        {
            switch (that)
            {
                case JoinEnum.FullJoin: return "FULL JOIN";
                case JoinEnum.RightJoin: return "RIGHT JOIN";
                case JoinEnum.LeftJoin: return "LEFT JOIN";
                case JoinEnum.Join: return "JOIN";
            }
            return "";
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
