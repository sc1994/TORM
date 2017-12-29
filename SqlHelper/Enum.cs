using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlHelper
{
    #region 键值关系
    /// <summary>
    /// 键值关系
    /// </summary>
    public enum RelationEnum
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("=")]
        Equal,
        /// <summary>
        /// 不等于
        /// </summary>
        [Description("<>")]
        NotEqual,
        /// <summary>
        /// in
        /// </summary>
        [Description("IN")]
        In,
        /// <summary>
        /// NotIn
        /// </summary>
        [Description("NOT IN")]
        NotIn,
        /// <summary>
        /// 大于
        /// </summary>
        [Description(">")]
        Greater,
        /// <summary>
        /// 大于等于
        /// </summary>
        [Description(">=")]
        GreaterEqual,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("<")]
        Less,
        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("<=")]
        LessEqual,
        /// <summary>
        /// 匹配
        /// </summary>
        [Description("LIKE")]
        Like,
        /// <summary>
        /// 右匹配
        /// </summary>
        [Description("LIKE")]
        RightLike,
        /// <summary>
        /// 左匹配
        /// </summary>
        [Description("LIKE")]
        LeftLike,
        /// <summary>
        /// 是
        /// </summary>
        [Description("IS")]
        IsNull,
        /// <summary>
        /// 不是
        /// </summary>
        [Description("IS NOT NULL")]
        IsNotNull
    }
    #endregion

    #region 向前条件的并存关系
    /// <summary>
    /// 向前条件的并存关系
    /// </summary>
    public enum CoexistEnum
    {
        /// <summary>
        /// AND 关系
        /// </summary>
        [Description("AND")]
        And,
        /// <summary>
        /// OR 关系
        /// </summary>
        [Description("OR")]
        Or
    }
    #endregion

    #region 排序关系
    /// <summary>
    /// 排序关系
    /// </summary>
    public enum SortEnum
    {
        /// <summary>
        /// 正序
        /// </summary>
        [Description("ASC")]
        Asc,
        /// <summary>
        /// 倒序
        /// </summary>
        [Description("DESC")]
        Desc,
    }
    #endregion

    #region 表链接 关系
    /// <summary>
    /// 表链接 关系
    /// </summary>
    public enum JoinEnum
    {
        /// <summary>
        /// 链接
        /// </summary>
        [Description("JOIN")]
        Join,
        /// <summary>
        /// 链接
        /// </summary>
        [Description("INNER JOIN")]
        InnerJoin,
        /// <summary>
        /// 左链接
        /// </summary>
        [Description("LEFT JOIN")]
        LeftJoin,
        /// <summary>
        /// 有链接
        /// </summary>
        [Description("RIGHT JOIN")]
        RightJoin
    }
    #endregion

    #region 错误枚举
    /// <summary>
    /// 错误枚举
    /// </summary>
    public enum ErrorEnum
    {
        /// <summary>
        /// 当前表结构缺少PrimaryKey
        /// </summary>
        [Description("当前表结构缺少PrimaryKey")]
        E1000,
        /// <summary>
        /// 当前操作必须传入条件限制
        /// </summary>
        [Description("当前操作必须传入条件限制")]
        E1001,
        /// <summary>
        /// 当前操作必须传入UPDATE字段和值
        /// </summary>
        [Description("当前操作必须传入UPDATE字段和值")]
        E1002,
        /// <summary>
        /// 当您尝试 JOIN 时,请先设置 Alia 值
        /// </summary>
        [Description("当您尝试 JOIN 时,请先设置 Alia 值")]
        E1003
    }
    #endregion
}
