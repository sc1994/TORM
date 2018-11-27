using ORM.Realizes;
using System;
using System.Linq;
using System.Text;

namespace ORM
{
    /// <summary>
    /// 杂项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Others<T> : RealizeCommon<T>
    {
        /// <summary>
        /// 自动生成表（迁移模式）
        /// </summary>
        public void AutoTable()
        {
            var tableType = ChenkT();
            var tableInfo = GetTableInfo();

            if (ExistTable())
            {
                //                var tableByDB = Query<FieldByDB>(
                //$@"SELECT COLUMN_NAME AS Name, IS_NULLABLE AS NotNullDB, DATA_TYPE AS Type, CHARACTER_MAXIMUM_LENGTH AS StringLength, NUMERIC_PRECISION AS Length, NUMERIC_SCALE AS PrecisionDB, EXTRA AS IdentityDB, COLUMN_COMMENT AS Comment
                //  FROM INFORMATION_SCHEMA.Columns 
                //  WHERE TABLE_NAME='{tableInfo.Table}' ;");
                //throw new NotImplementedException(); todo 以后考虑实现
                return;
            }

            var properties = tableType.GetProperties();
            var sql = new StringBuilder($"CREATE TABLE {tableInfo.DB}.{tableInfo.Table} (");
            foreach (var item in properties)
            {
                var fieldInfo = GetFieldInfo(item);
                if (!string.IsNullOrWhiteSpace(fieldInfo.Foreign))
                    continue; // 屏蔽掉设置了外键的字段 todo 考虑设置主外键
                if (fieldInfo.Key)
                {
                    sql.Append($"\r\n  PRIMARY KEY ({fieldInfo.Name}),");
                }

                sql.Append($"\r\n  {fieldInfo.Name} {SwithDataType(fieldInfo)}");
                if (fieldInfo.NotNull)
                {
                    sql.Append(" NOT NULL");
                }
                if (fieldInfo.Identity) // 自增和默认值不能并存
                {
                    sql.Append(" AUTO_INCREMENT");
                }
                else if (!string.IsNullOrWhiteSpace(fieldInfo.DefaultValue))
                {
                    sql.Append($" DEFAULT '{fieldInfo.DefaultValue}'");
                }
                if (!string.IsNullOrWhiteSpace(fieldInfo.Comment))
                {
                    sql.Append($" COMMENT '{fieldInfo.Comment}'");
                }
                sql.Append(",");
            }
            sql.SafeRemove(sql.Length - 1, 1);
            sql.Append("\r\n)\r\nENGINE = INNODB;");
            Execute(sql.ToString());
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <returns></returns>
        private bool ExistTable()
        {
            var tableInfo = GetTableInfo();
            if (tableInfo.DBType == DBTypeEnum.MySQL)
            {
                var tableName = Query<string>($"SELECT table_name FROM information_schema.TABLES WHERE table_name ='{tableInfo.Table}';");
                return tableName.FirstOrDefault() == tableInfo.Table;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 切换数据类型
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private string SwithDataType(FieldInfo field)
        {
            switch (field.Type)
            {
                case "string":
                    return $"VARCHAR({DefaultLength(field.Length, 255)})";
                case "int64":
                    return $"BIGINT({DefaultLength(field.Length, 20)})";
                case "int32":
                    return $"INT({DefaultLength(field.Length, 11)})";
                case "int16":
                    return $"TINYINT({DefaultLength(field.Length, 4)})";
                case "boolean":
                    return "BOOLEAN";
                case "float":
                    return "FLOAT";
                case "double":
                    return "DOUBLE";
                case "decimal":
                    return $"DECIMAL({DefaultLength(field.Length, 18)}, {DefaultLength(field.Precision, 2)})";
                case "datetime":
                    return "DATETIME";
            }
            throw new NotImplementedException("未实现的数据类型");
        }

        /// <summary>
        /// 转到默认长度
        /// </summary>
        /// <param name="length"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        private int DefaultLength(int length, int def)
        {
            if (length < 1)
            {
                return def;
            }
            return length;
        }

        //private IEnumerable<FieldInfo> DifferentFields(IEnumerable<FieldInfo> that, IEnumerable<FieldByDB> db)
        //{
        //    var result = new List<FieldInfo>();

        //    return result;
        //}
    }
}
