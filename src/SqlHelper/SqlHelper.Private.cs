using System;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;

namespace SqlHelper
{
    public partial class SqlHelper<T> where T : class
    {
        private readonly T _model = Activator.CreateInstance<T>();
        private readonly string _tableName;
        private readonly Dictionary<string, object> _updateList = new Dictionary<string, object>();
        private readonly List<WhereDictionary> _whereList = new List<WhereDictionary>();
        private readonly List<string> _updateStr = new List<string>();
        private readonly List<string> _whereStr = new List<string>();
        private readonly List<string> _showStr = new List<string>();
        private readonly List<string> _joinStr = new List<string>();
        private readonly List<JoinDictionary> _joinList = new List<JoinDictionary>();
        private readonly List<string> _sortStr = new List<string>();
        private readonly Dictionary<string, SortEnum> _sortList = new Dictionary<string, SortEnum>();
        private readonly List<string> _groupStr = new List<string>();
        private readonly Type _properties;

        private void GetSelectSql(string where, string show = "")
        {
            var top = Top > 0 ? $"TOP ({Top})" : "";
            SqlString = new StringBuilder();
            SqlString.Append(IsNullOrEmpty(show)
                ? $" SELECT {top} * FROM {_tableName} WHERE 1=1 {where}"
                : $" SELECT {top} {show} FROM {_tableName} WHERE 1=1 {where}");
        }

        private void GetSelectSql(string field, object value, string show = "")
        {
            SqlString = new StringBuilder();
            var top = Top > 0 ? $"TOP ({Top})" : "";
            SqlString.Append(IsNaN(value)
                ? (IsNullOrEmpty(show)
                    ? $" SELECT {top} * FROM {_tableName} WHERE {field} = '{value}' "
                    : $" SELECT {top} {show} FROM {_tableName} WHERE {field} = '{value}' ")
                : (IsNullOrEmpty(show)
                    ? $" SELECT {top} * FROM {_tableName} WHERE {field} = {value} "
                    : $" SELECT {top} {show} FROM {_tableName} WHERE {field} = {value} "));
        }

        private string GetIdentityKey()
        {
            var properties = _properties.GetProperties();
            var identityKey = properties.FirstOrDefault(x => x.Name == "IdentityKey");
            if (identityKey != null)
            {
                return identityKey.Name;
            }
            var field = string.Empty;
            var strSql =
                $@"USE {ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString};
                   SELECT ( CASE WHEN COLUMNPROPERTY(id, name, 'IsIdentity') = 1
                                               THEN '1'
                                               ELSE '0'
                                          END ) as identityKey,
                                        name
                              FROM      syscolumns
                              WHERE     id = OBJECT_ID ('{_tableName}') ";
            try
            {
                var list = DbClient.Query<dynamic>(strSql);
                var model = list.FirstOrDefault(x => x.identityKey.ToString() == "1");
                if (model != null) field = (model.name ?? "").ToString();
                return field;
            }
            catch (Exception)
            {
                return "";
            }
        }
        
        private string GetPrimaryKey()
        {
            var properties = _properties.GetProperties();
            var primaryKey = properties.FirstOrDefault(x => x.Name == "PrimaryKey");
            if (primaryKey != null)
            {
                return primaryKey.Name;
            }
            var strSql = $"USE {ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString};EXEC sp_pkeys @table_name='{RemoveStrModel(_properties.UnderlyingSystemType.Name)}'";
            try
            {
                return DbClient.Query<dynamic>(strSql).FirstOrDefault()?.COLUMN_NAME.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        private bool IsNaN(object value)
            => !(value is int 
            || value is long 
            || value is double 
            || value is float 
            || value is byte 
            || value is decimal 
            || value is short 
            || value is ushort 
            || value is sbyte);

        private StringBuilder RemoveEndNumber(StringBuilder sbStr, int number)
        {
            if (sbStr.Length < number)
            {
                return sbStr;
            }
            return sbStr.Remove(sbStr.Length - number, number);
        }

        private string GetDescription(Enum enumItemName)
        {
            var fi = enumItemName.GetType().GetField(enumItemName.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return enumItemName.ToString();
        }

        private SqlAndParameter GetUpdateString()
        {
            var re = new SqlAndParameter();
            var count = 0;
            foreach (var update in _updateList)
            {
                re.SqlStr += $"{update.Key}=@{update.Key}{count},";
                re.Parameter.Add($"@{update.Key}{count}", update.Value);
                count++;
            }
            foreach (var update in _updateStr)
            {
                re.SqlStr += update + ",";
            }
            re.SqlStr = re.SqlStr.TrimEnd(',');
            return re;
        }

        private SqlAndParameter GetWhereString()
        {
            var re = new SqlAndParameter();
            var count = 0;
            foreach (var where in _whereList)
            {
                if (where.Relation == RelationEnum.In)
                {
                    re.SqlStr += $"{GetDescription(where.Coexist)} {where.Field} IN (@{where.Field.Replace(".", "_")}{count}) ";
                }
                else if (where.Relation == RelationEnum.NotIn)
                {
                    re.SqlStr += $"{GetDescription(where.Coexist)} {where.Field} NOT IN (@{where.Field.Replace(".", "_")}{count}) ";
                }
                else if (where.Relation == RelationEnum.IsNotNull || where.Relation == RelationEnum.IsNull)
                {
                    re.SqlStr += $"{GetDescription(where.Coexist)} {where.Field} {GetDescription(where.Relation)} ";
                }
                else if (where.Relation == RelationEnum.Like)
                {
                    re.SqlStr += $"{GetDescription(where.Coexist)} {where.Field} LIKE '%'+@{where.Field.Replace(".", "_")}{count}+'%' ";
                }
                else if (where.Relation == RelationEnum.LeftLike)
                {
                    re.SqlStr += $"{GetDescription(where.Coexist)} {where.Field} LIKE '%'+@{where.Field.Replace(".", "_")}{count} ";
                }
                else if (where.Relation == RelationEnum.RightLike)
                {
                    re.SqlStr += $"{GetDescription(where.Coexist)} {where.Field} LIKE @{where.Field.Replace(".", "_")}{count}+'%' ";
                }
                else
                {
                    re.SqlStr += $"{GetDescription(where.Coexist)} {where.Field} {GetDescription(where.Relation)} @{where.Field.Replace(".", "_")}{count} ";
                }
                re.Parameter.Add($"@{where.Field.Replace(".", "_")}{count}", where.Value);
                count++;
            }
            foreach (var where in _whereStr)
            {
                re.SqlStr += where;
            }

            return re;
        }

        private string GetShowString()
        {
            var str = string.Join(",", _showStr).TrimEnd(',');
            return IsNullOrEmpty(str) ? " * " : str;
        }

        private string GetSortString()
        {
            var strSql = _sortList
                .Aggregate("",
                    (current, sort) =>
                        current + $"{sort.Key} {GetDescription(sort.Value)},");
            return _sortStr.Aggregate(strSql, (current, sort) => current + $"{sort},").TrimEnd(',');
        }

        private string GetGroupString()
            => _groupStr.Aggregate("", (current, group) => current + group + ",").TrimEnd(',');

        private string GetJoinString()
        {
            var strSql = $" {Alia} ";
            strSql = _joinList
                .Aggregate(strSql,
                    (current, join)
                        => current +
                           $@" {GetDescription(join.RelationJoin)} {ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString}.dbo.[{join.ThatTable}] 
                    {join.ThatAlia} ON {Alia}.{join.RelationField} = {join.ThatAlia}.{join.ThatRelationField} {join.Where}");
            if (_joinStr.Any())
            {
                foreach (var str in _joinStr)
                {
                    strSql += " " + str;
                }
            }
            return strSql;
        }

        /// <summary>
        /// 移除 T 后面可能出现的个别字符串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string RemoveStrModel(string name)
        {
            var re = name.ToLower();
            if (re.IndexOf("model", StringComparison.Ordinal) == re.Length - 5)
            {
                re = name.Remove(name.Length - 5);
            }
            if (re.Remove(0, re.Length - 1) == "_" || re.Remove(0, re.Length - 1) == ".")
            {
                re = name.Remove(name.Length - 1);
            }
            return re;
        }

        private bool IsNullOrEmpty(string str)
        {
            return string.IsNullOrEmpty(str.Trim());
        }
    }
}
