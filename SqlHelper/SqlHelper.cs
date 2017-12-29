using Dapper;
using System;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;

namespace SqlHelper
{
    /// <summary>
    /// sql 帮助类 方便sql 的增删改查 操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SqlHelper<T> where T : class
    {
        #region 私有变量
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
        #endregion

        #region 共有变量
        /// <summary>
        /// 获取自增键
        /// </summary>
        public string IdentityKey { get; }

        /// <summary>
        /// 获取主键
        /// </summary>
        public string PrimaryKey { get; }

        /// <summary>
        /// 获取或设置 top 值
        /// </summary>
        public int Top { get; set; } = 0;

        /// <summary>
        /// 需要分页时的配置
        /// </summary>
        public PageConfig PageConfig { get; set; } = new PageConfig();

        /// <summary>
        /// 获取分页数据总数
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// 方便与排查异常
        /// </summary>
        public StringBuilder SqlString { get; private set; } = new StringBuilder();

        /// <summary>
        /// 主表的别名 使用到AddJoin时,必选设置此属性
        /// </summary>
        public string Alia { get; set; } = string.Empty;
        #endregion


        #region 构造函数
        /// <summary>
        /// 初始化实例
        /// </summary>
        public SqlHelper()
        {
            _properties = _model.GetType();
            if (_model != null)
                _tableName =
                    $"{ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString}.dbo.[{RemoveStrModel(_properties.UnderlyingSystemType.Name)}]";
            PrimaryKey = GetPrimaryKey();
            IdentityKey = GetIdentityKey();
        }

        /// <summary>
        /// 初始化实例 (传入表名: 适用与实体名和表名有差异的时候)
        /// </summary>
        /// <param name="tableName"></param>
        public SqlHelper(string tableName)
        {
            _properties = _model.GetType();
            _tableName = $"{ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString}.dbo.[{tableName}]";
            PrimaryKey = GetPrimaryKey();
            IdentityKey = GetIdentityKey();
        }

        /// <summary>
        /// 初始化实例(需要 实例对象)
        /// </summary>
        /// <param name="t"></param>
        public SqlHelper(T t)
        {
            _model = t;
            _properties = _model.GetType();
            _tableName =
                $"{ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString}.dbo.[{RemoveStrModel(_properties.UnderlyingSystemType.Name)}]";
            PrimaryKey = GetPrimaryKey();
            IdentityKey = GetIdentityKey();

        }

        /// <summary>
        /// 初始化实例(需要 实例对象，主键)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="primaryKey"></param>
        public SqlHelper(T t, string primaryKey)
        {
            _model = t;
            _properties = _model.GetType();
            _tableName =
                $"{ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString}.dbo.[{RemoveStrModel(_properties.UnderlyingSystemType.Name)}]";
            PrimaryKey = primaryKey;
            IdentityKey = GetIdentityKey();
        }

        /// <summary>
        /// 初始化实例(需要 实例对象，主键，自增键)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="primaryKey"></param>
        /// <param name="identityKey"></param>
        public SqlHelper(T t, string primaryKey, string identityKey)
        {
            _model = t;
            _properties = _model.GetType();
            _tableName =
                $"{ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString}.dbo.[{RemoveStrModel(_properties.UnderlyingSystemType.Name)}]";
            PrimaryKey = primaryKey;
            IdentityKey = identityKey;
        }

        /// <summary>
        /// 初始化实例(需要 实例对象，主键，自增键，表名)
        /// </summary>
        /// <param name="t"></param>
        /// <param name="primaryKey"></param>
        /// <param name="identityKey"></param>
        /// <param name="tableName"></param>
        public SqlHelper(T t, string primaryKey, string identityKey, string tableName)
        {
            _model = t;
            PrimaryKey = primaryKey;
            IdentityKey = identityKey;
            _tableName = $"{ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString}.dbo.[{tableName}]";
        }
        #endregion


        #region Where
        /// <summary>
        /// 添加条件 (不支持参数化)
        /// </summary>
        /// <param name="where">适用于复杂的添加关系,如 AND (x=x OR a=a)</param>
        public void Where(string where)
        {
            _whereStr.Add(where);
        }

        /// <summary>
        /// 添加条件 
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        public void Where(string field, object value)
        {
            _whereList.Add(new WhereDictionary
            {
                Field = field,
                Value = value,
                Relation = RelationEnum.Equal,
                Coexist = CoexistEnum.And
            });
        }

        /// <summary>
        /// 添加条件 
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="relation">字段以及值之间的关系</param>
        public void Where(string field, object value, RelationEnum relation)
        {
            _whereList.Add(new WhereDictionary
            {
                Field = field,
                Value = value,
                Relation = relation,
                Coexist = CoexistEnum.And
            });
        }

        /// <summary>
        /// 添加条件 
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="relation">字段以及值之间的关系</param>
        /// <param name="coexist">此条件相对于之前条件之间的关系</param>
        public void Where(string field, object value, RelationEnum relation, CoexistEnum coexist)
        {
            _whereList.Add(new WhereDictionary
            {
                Field = field,
                Value = value,
                Relation = relation,
                Coexist = coexist
            });
        }

        /// <summary>
        /// 添加条件 
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        public void Where(Enum field, object value)
        {
            _whereList.Add(new WhereDictionary
            {
                Field = field.ToString(),
                Value = value,
                Relation = RelationEnum.Equal,
                Coexist = CoexistEnum.And
            });
        }

        /// <summary>
        /// 添加条件 
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="relation">字段以及值之间的关系</param>
        public void Where(Enum field, object value, RelationEnum relation)
        {
            _whereList.Add(new WhereDictionary
            {
                Field = field.ToString(),
                Value = value,
                Relation = relation,
                Coexist = CoexistEnum.And
            });
        }

        /// <summary>
        /// 添加条件 
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="relation">字段以及值之间的关系</param>
        /// <param name="coexist">此条件相对于之前条件之间的关系</param>
        public void Where(Enum field, object value, RelationEnum relation, CoexistEnum coexist)
        {
            _whereList.Add(new WhereDictionary
            {
                Field = field.ToString(),
                Value = value,
                Relation = relation,
                Coexist = coexist
            });
        }
        #endregion

        #region Insert
        /// <summary>
        /// 插入一条数据 (数据来自初始化传入的对象实体)
        /// </summary>
        /// <returns></returns>
        public int Insert()
        {
            SqlString = new StringBuilder();
            var properties = _properties.GetProperties();
            var fields = string.Empty;
            var values = string.Empty;
            var para = new DynamicParameters();
            foreach (var t in properties)
            {
                if (IsNullOrEmpty(IdentityKey) || IdentityKey != t.Name)
                {
                    fields += $"{t.Name},";
                    values += $"@{t.Name},";
                    para.Add("@" + t.Name, t.GetValue(_model, null));
                }
            }
            SqlString.Append($" INSERT INTO {_tableName} ({fields.TrimEnd(',')}) VALUES ({values.TrimEnd(',')});");
            if (!IsNullOrEmpty(IdentityKey))
            {
                SqlString.Append(" SELECT @@IDENTITY; ");
            }
            return DbClient.Query<int>(SqlString.ToString(), para).FirstOrDefault();
        }
        #endregion

        #region UPDATE
        /// <summary>
        /// 更新一条数据 (需要依赖主键)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(T model)
        {
            if (IsNullOrEmpty(IdentityKey)) throw new Exception(GetDescription(ErrorEnum.E1000));
            SqlString = new StringBuilder();
            SqlString.Append($" UPDATE {_tableName} SET ");
            var properties = model.GetType().GetProperties();
            var para = new DynamicParameters();
            var keyValue = new object();
            foreach (var t in properties)
            {
                if (IdentityKey != t.Name && PrimaryKey != t.Name)
                {
                    SqlString.Append($" {t.Name} = @{t.Name},");
                    para.Add("@" + t.Name, t.GetValue(model, null));
                }
                else
                {
                    if (t.Name == PrimaryKey)
                    {
                        keyValue = t.GetValue(model, null);
                    }
                }
            }
            SqlString = RemoveEndNumber(SqlString, 1);
            SqlString.Append($" WHERE {PrimaryKey} = @{PrimaryKey}_Key ");
            para.Add($"@{PrimaryKey}_Key", keyValue);
            return DbClient.Excute(SqlString.ToString(), para);
        }

        /// <summary>
        /// 添加一项更新字段
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        public void AddUpdate(string field, object value)
        {
            _updateList.Add(field, value);
        }

        /// <summary>
        /// 添加一项更新字段
        /// </summary>
        /// <param name="update">sql</param>
        public void AddUpdate(string update)
        {
            _updateStr.Add(update);
        }

        /// <summary>
        /// 执行更新(必须AddUpdate 必须AddWhere)
        /// </summary>
        /// <returns></returns>
        public int Update()
        {
            SqlString = new StringBuilder();
            var top = Top > 0 ? $"TOP({Top})" : "";
            SqlString.Append($"UPDATE {top} {_tableName} SET ");
            var para = new DynamicParameters();
            var sp = GetUpdateString();
            if (IsNullOrEmpty(sp.SqlStr))
            {
                throw new Exception(GetDescription(ErrorEnum.E1002));
            }
            SqlString.Append($"{sp.SqlStr}");
            para.AddDynamicParams(sp.Parameter);
            sp = GetWhereString();
            if (IsNullOrEmpty(sp.SqlStr))
            {
                throw new Exception(GetDescription(ErrorEnum.E1001));
            }
            SqlString.Append(" WHERE 1=1 " + sp.SqlStr);
            para.AddDynamicParams(sp.Parameter);
            return DbClient.Excute(SqlString.ToString(), para);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">where条件 以AND开头</param>
        /// <returns></returns>
        public int Delete(string where)
        {
            if (IsNullOrEmpty(where)) throw new Exception(GetDescription(ErrorEnum.E1001));
            SqlString = new StringBuilder();
            SqlString.Append($"DELETE {_tableName} WHERE 1=1 {where} ");
            return DbClient.Excute(SqlString.ToString());
        }

        /// <summary>
        /// 依据主键删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteByPrimaryKey(object id)
        {
            if (IsNullOrEmpty(PrimaryKey)) throw new Exception(GetDescription(ErrorEnum.E1000));
            SqlString = new StringBuilder();
            SqlString.Append($"DELETE {_tableName} WHERE {PrimaryKey} = @key ");
            return DbClient.Excute(SqlString.ToString(), new { key = id });
        }

        /// <summary>
        /// 批量删除 (必须:Where)
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            if (!_whereList.Any() && !_whereStr.Any()) throw new Exception(GetDescription(ErrorEnum.E1001));

            SqlString = new StringBuilder();
            var sq = GetWhereString();
            SqlString.Append($"DELETE {_tableName} WHERE 1=1 {sq.SqlStr}");
            var para = new DynamicParameters();
            para.AddDynamicParams(sq.Parameter);
            return DbClient.Excute(SqlString.ToString(), para);

        }
        #endregion

        #region SELECT
        /// <summary>
        /// 添加显示字段
        /// </summary>
        /// <param name="shows"></param>
        public void Show(string shows)
        {
            _showStr.Add(shows);
        }

        /// <summary>
        /// 添加显示字段
        /// </summary>
        /// <param name="show"></param>
        public void Show(Enum show)
        {
            _showStr.Add(show.ToString());
        }

        /// <summary>
        /// 添加表链接关系
        /// </summary>
        /// <param name="join"></param>
        public void AddJoin(string join)
        {
            _joinStr.Add(join);
        }

        /// <summary>
        /// 添加表连接关系
        /// </summary>
        /// <param name="relationJoin">连接关系</param>
        /// <param name="thatTable">另一个表</param>
        /// <param name="thatAlia">另一个别名</param>
        /// <param name="relationField">主关联字段</param>
        /// <param name="thatRelationField">另一个关联字段</param>
        /// <param name="where">链接之前的筛选(以AND开头)</param>
        public void AddJoin(JoinEnum relationJoin, string thatTable, string thatAlia, string relationField, string thatRelationField, string where = "")
        {
            _joinList.Add(new JoinDictionary
            {
                RelationJoin = relationJoin,
                RelationField = relationField,
                ThatAlia = thatAlia,
                ThatRelationField = thatRelationField,
                ThatTable = thatTable,
                Where = where
            });
        }

        /// <summary>
        /// 添加排序字段
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(string order)
        {
            _sortStr.Add(order);
        }

        /// <summary>
        /// 添加排序字段
        /// </summary>
        /// <param name="field"></param>
        /// <param name="sort">排序关系</param>
        public void AddOrder(string field, SortEnum sort)
        {
            _sortList.Add(field, sort);
        }

        /// <summary>
        /// 添加排序字段
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Enum order)
        {
            _sortStr.Add(order.ToString());
        }

        /// <summary>
        /// 添加排序字段
        /// </summary>
        /// <param name="field"></param>
        /// <param name="sort">排序关系</param>
        public void AddOrder(Enum field, SortEnum sort)
        {
            _sortList.Add(field.ToString(), sort);
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="field"></param>
        public void AddGroup(string field)
        {
            _groupStr.Add(field);
        }

        /// <summary>
        /// 查询数据 (需要分页请先配置PageConfig)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Select()
        {
            var totlaSql = "";
            var isPage = PageConfig.PageIndex > 0 && PageConfig.PageSize > 0;
            SqlString = new StringBuilder();
            if (isPage)
            {
                var pageSortField = IsNullOrEmpty(PageConfig.PageSortSql)
                    ? $" {PageConfig.PageSortField} {GetDescription(PageConfig.SortEnum)} "
                    : PageConfig.PageSortSql;

                SqlString.Append("SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY " +
                                 $" {pageSortField} " +
                                 $" ) AS ROWNUMBER ,{GetShowString()} FROM {_tableName} ");
                totlaSql += $"SELECT COUNT(1) FROM {_tableName} ";
            }
            else
            {
                var top = Top > 0 ? $"TOP({Top})" : "";
                SqlString.Append($"SELECT {top} {GetShowString()} FROM {_tableName} ");
            }
            var join = GetJoinString();
            if (!IsNullOrEmpty(join.Trim()))
            {
                if (IsNullOrEmpty(Alia.Trim()))
                {
                    throw new Exception(GetDescription(ErrorEnum.E1003));
                }
                SqlString.Append(join);
                totlaSql += join;
            }
            var para = new DynamicParameters();
            var sp = GetWhereString();
            para.AddDynamicParams(sp.Parameter);
            SqlString.Append(" WHERE 1=1 " + sp.SqlStr);
            totlaSql += " WHERE 1=1 " + sp.SqlStr;
            var group = GetGroupString();
            if (!IsNullOrEmpty(group.Trim()))
            {
                SqlString.Append(" GROUP BY " + group);
                totlaSql += " GROUP BY " + group;
            }
            var sort = GetSortString();
            if (!IsNullOrEmpty(sort.Trim()))
            {
                SqlString.Append(" ORDER BY " + sort);
            }
            if (isPage)
            {
                SqlString.Append($") A WHERE ROWNUMBER BETWEEN {(PageConfig.PageIndex - 1) * PageConfig.PageSize + 1} AND {PageConfig.PageIndex * PageConfig.PageSize} ");
                Total = DbClient.Query<int>(totlaSql, para).FirstOrDefault();
            }
            return DbClient.Query<T>(SqlString.ToString(), para);
        }

        #endregion

        #region 简单查询
        /// <summary>
        /// 简单查询 (单个条件)
        /// </summary>
        /// <param name="field">筛选字段</param>
        /// <param name="value">筛选值</param>
        /// <param name="show">显示字段</param>
        /// <returns></returns>
        public IEnumerable<T> GetModelList(string field, object value, string show = "")
        {
            GetSelectSql(field, value, show);
            return DbClient.Query<T>(SqlString.ToString());
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="key">主键OR自增键</param>
        /// <param name="value">值</param>
        /// <param name="show">显示字段</param>
        /// <returns></returns>
        public T GetModel(string key, object value, string show = "")
        {
            GetSelectSql(key, value, show);
            return DbClient.Query<T>(SqlString.ToString()).FirstOrDefault();
        }
        #endregion

        #region 针对一些不太好生成的 sql 可选用此中的方法, 自定义性质比较强
        /// <summary>
        /// 直接执行一条SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> GetModelListBySql(string sql)
            => DbClient.Query<T>(sql);

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="where">条件(以 AND 开头)</param>
        /// <param name="show">显示字段</param>
        /// <returns></returns>
        public IEnumerable<T> GetModelList(string where, string show = "")
        {
            GetSelectSql(where, show);
            return DbClient.ExecuteScalar<IEnumerable<T>>(SqlString.ToString());
        }

        /// <summary>
        /// 获取匿名对象
        /// </summary>
        /// <param name="where">条件(以 AND 开头)</param>
        /// <param name="show">显示字段</param>
        /// <returns></returns>
        public IEnumerable<dynamic> GetDynamic(string where, string show = "")
        {
            GetSelectSql(where, show);
            return DbClient.Query<dynamic>(SqlString.ToString());
        }
        #endregion

        #region 私有方法
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
            => !(value is int || value is long || value is double || value is float || value is byte || value is decimal || value is short || value is ushort || value is sbyte);

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
        #endregion
    }
}
