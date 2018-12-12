using System;
using System.Linq;
using System.Reflection;

namespace ORM.Realizes
{
    /// <summary>
    /// 基础信息
    /// </summary>
    public partial class RealizeCommon<T>
    {
        /// <summary>
        /// 取主表
        /// </summary>
        /// <returns></returns>
        protected string GetTableName()
        {
            return GetTableName(_t);
        }

        /// <summary>
        /// 取指定表名
        /// </summary>
        /// <returns></returns>
        protected string GetTableName(Type table)
        {
            return GetTableInfo(table).Name;
        }

        /// <summary>
        /// 依据特性获取表信息
        /// </summary>
        /// <returns></returns>
        internal TableInfo GetTableInfo()
        {
            return GetTableInfo(_t);
        }

        /// <summary>
        /// 依据特性获取表信息
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        internal TableInfo GetTableInfo(Type table)
        {
            if (Stores.TableInfoDic.TryGetValue(table.MetadataToken, out var value))
            {
                return value;
            }

            var attribute = table.GetCustomAttributes(typeof(TableAttribute), false).FirstOrDefault();

            if (attribute == null)
            {
                throw new Exception("需指定表的特性，使用 TableAttribute 。");
            }

            var info = (TableAttribute)attribute;
            var fields = typeof(T).GetProperties().Select(GetFieldInfo);
            var r = new TableInfo
            {
                DB = info.DB,
                DBType = info.DBType,
                Name = string.IsNullOrWhiteSpace(info.Table) ? table.Name : info.Table,
                ConnectionString = Stores.DbConfigDic[info.DB],
                Key = fields.FirstOrDefault(x => x.Key),
                Identity = fields.FirstOrDefault(x => x.Identity)
            };
            Stores.TableInfoDic.TryAdd(table.MetadataToken, r);
            return r;
        }

        /// <summary>
        /// 获取字段属性
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        internal FieldInfo GetFieldInfo(PropertyInfo field)
        {
            if (Stores.FieldInfoDic.TryGetValue(field.MetadataToken, out var result))
            {
                return result;
            }

            result = new FieldInfo
            {
                Key = field.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() != null,
                Identity = field.GetCustomAttributes(typeof(IdentityAttribute), true).FirstOrDefault() != null,
                Type = field.PropertyType.Name.ToLower()
            };
            var fieldInfo = field.GetCustomAttributes(typeof(FieldAttribute), true).FirstOrDefault();
            var foreign = field.GetCustomAttributes(typeof(ForeignAttribute), true).FirstOrDefault();
            if (fieldInfo is FieldAttribute value)
            {
                result.Name = string.IsNullOrWhiteSpace(value.Alias) ? field.Name : value.Alias;
                result.Comment = value.Comment;
                result.DefaultValue = value.DefaultValue;
                result.NotNull = value.NotNull;
                result.Length = value.Length;
                result.Precision = value.Precision;
            }
            else if (foreign is ForeignAttribute fValue)
            {
                result.Foreign = fValue.Foreign;
            }

            if (string.IsNullOrWhiteSpace(result.Name))
            {
                result.Name = field.Name;
            }
            Stores.FieldInfoDic.TryAdd(field.MetadataToken, result);
            return result;
        }
    }
}
