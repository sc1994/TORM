using System;
using System.Collections.Generic;
using System.Text;

namespace ORM
{
    /// <summary>
    /// 表属性标记
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 数据库
        /// </summary>
        internal string DB { get; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        internal DBTypeEnum DBType { get; }
        /// <summary>
        /// 表名
        /// </summary>
        internal string Table { get; }

        public TableAttribute(string db, DBTypeEnum dbType)
        {
            DB = db;
            DBType = dbType;
        }

        public TableAttribute(string db, DBTypeEnum dbType, string table)
        {
            DB = db;
            DBType = dbType;
            Table = table;
        }
    }

    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
    }

    /// <summary>
    /// 自增
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentityAttribute : Attribute
    {
    }

    /// <summary>
    /// 表属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
        internal string Alias { get; set; }
        internal string DefaultValue { get; set; }
        internal bool NotNull { get; set; }
        internal string Comment { get; set; }
        /// <summary>
        /// 表属性
        /// </summary>
        /// <param name="alias">别名</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="notNull">不可为空</param>
        /// <param name="comment">描述</param>
        public FieldAttribute(string alias = null, string defaultValue = null, bool notNull = true, string comment = null)
        {
            Alias = alias;
            DefaultValue = defaultValue;
            NotNull = notNull;
            Comment = comment;
        }
    }
}
