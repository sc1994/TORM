using System;

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

        /// <summary>
        /// 表对应实体特性
        /// </summary>
        /// <param name="db">指定数据库</param>
        /// <param name="dbType">指定数据库类型</param>
        public TableAttribute(string db, DBTypeEnum dbType)
        {
            DB = db;
            DBType = dbType;
        }

        /// <summary>
        /// 表对应实体特性
        /// </summary>
        /// <param name="db">指定数据库</param>
        /// <param name="dbType">指定数据库类型</param>
        /// <param name="table">指定表名</param>
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
        internal int Length { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        internal int Precision { get; set; }

        /// <summary>
        /// 表属性
        /// </summary>
        /// <param name="Alias">别名</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="NotNull">不可为空</param>
        /// <param name="Comment">描述</param>
        /// <param name="Length">长度</param>
        /// <param name="Precision">精度</param>
        public FieldAttribute(
            string Alias = null,
            string DefaultValue = null,
            bool NotNull = true,
            string Comment = null,
            int Length = 0,
            int Precision = 0)
        {
            this.Alias = Alias;
            this.DefaultValue = DefaultValue;
            this.NotNull = NotNull;
            this.Comment = Comment;
            this.Length = Length;
            this.Precision = Precision;
        }
    }

    /// <summary>
    /// 指定外键
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignAttribute : Attribute
    {
        internal string Foreign { get; set; }
        /// <summary>
        /// 指定外键
        /// </summary>
        /// <param name="foreign"></param>
        public ForeignAttribute(string foreign)
        {
            Foreign = foreign;
        }
    }
}
