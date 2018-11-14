using System;

namespace ORM
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public string DB { get; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DBTypeEnum DBType { get; }
        /// <summary>
        /// 表名
        /// </summary>
        public string Table { get; }

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

    public enum DBTypeEnum
    {
        MySQL,
        SQLServer2008,
        SQLServer2012
    }
}
