using System;
using System.Linq.Expressions;

namespace ORM
{
    /// <summary>
    /// 联查
    /// </summary>
    public enum JoinEnum
    {
        /// <summary>
        /// JOIN LEFT
        /// </summary>
        LeftJoin,
        /// <summary>
        /// JOIN RIGHT
        /// </summary>
        RightJoin,
        /// <summary>
        /// JOIN 
        /// </summary>
        Join,
        /// <summary>
        /// JOIN FULL
        /// </summary>
        FullJoin
    }

    /// <summary>
    /// 排序
    /// </summary>
    public enum OrderEnum
    {
        /// <summary>
        /// ASC
        /// </summary>
        Asc,
        /// <summary>
        /// DESC
        /// </summary>
        Desc
    }

    /// <summary>
    /// sql 类型，用做存储sql的key
    /// </summary>
    public enum SqlTypeEnum
    {
        /// <summary>
        /// Select
        /// </summary>
        Select,
        /// <summary>
        /// Where
        /// </summary>
        Where,
        /// <summary>
        /// Order
        /// </summary>
        Order,
        /// <summary>
        /// Group
        /// </summary>
        Group,
        /// <summary>
        /// Join
        /// </summary>
        Join,
        /// <summary>
        /// Having
        /// </summary>
        Having,
        /// <summary>
        /// Set
        /// </summary>
        Set
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DBTypeEnum
    {
        /// <summary>
        /// MYSQL
        /// </summary>
        MySQL,
        /// <summary>
        /// MSSQL 2008及以下版本
        /// </summary>
        SQLServer2008,
        /// <summary>
        /// MSSQL 2012及以上版本
        /// </summary>
        SQLServer2012
    }

    static class EnumHelper
    {
        public static string ToExplain(this OrderEnum that)
        {
            switch (that)
            {
                case OrderEnum.Asc: return "ASC";
                case OrderEnum.Desc: return "DESC";
            }
            return "";
        }

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
