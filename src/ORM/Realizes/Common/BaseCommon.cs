using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ORM.Realizes
{
    /// <summary>
    /// 基础信息
    /// </summary>
    public partial class RealizeCommon<T>
    {
        /// <summary>
        /// 存放 join 表达式
        /// </summary>
        protected List<(Expression, JoinEnum)> _join = new List<(Expression, JoinEnum)>();
        /// <summary>
        /// 存放 select 表达式
        /// </summary>
        protected List<Expression> _selects = new List<Expression>();
        /// <summary>
        /// 存放 有别名的 select 表达式
        /// </summary>
        protected List<(Expression, string)> _selectAlias = new List<(Expression, string)>();
        /// <summary>
        /// 存放 order 表达式
        /// </summary>
        protected List<(Expression, OrderEnum)> _orders = new List<(Expression, OrderEnum)>();
        /// <summary>
        /// 存放 group 表达式
        /// </summary>
        protected List<Expression> _groups = new List<Expression>();
        /// <summary>
        /// 存放 where 表达式
        /// </summary>
        protected List<Expression> _where = new List<Expression>();
        /// <summary>
        /// 存放 having 表达式
        /// </summary>
        protected List<Expression> _having = new List<Expression>();
        /// <summary>
        /// 存放 set 表达式
        /// </summary>
        protected List<(Expression, object)> _set = new List<(Expression, object)>();
        /// <summary>
        /// 存放参数
        /// </summary>
        protected Dictionary<string, object> _params = new Dictionary<string, object>();
        /// <summary>
        /// 获取 T 属性，避免每次都计算
        /// </summary>
        protected Type _t = typeof(T);
        /// <summary>
        /// 已经用到的表（为了筛选当前 join 的那个表）
        /// </summary>
        protected List<Type> useTables = new List<Type> { typeof(T) };
        /// <summary>
        /// 全部的表
        /// </summary>
        protected List<Type> allTables = new List<Type>();
        /// <summary>
        /// sql（为了防止多次调用同一个方法而多次解析，将sql存放在这边）
        /// </summary>
        private readonly Dictionary<SqlTypeEnum, StringBuilder> _sqlDic = new Dictionary<SqlTypeEnum, StringBuilder>();





        /// <summary>
        /// 验证T且返回类型
        /// </summary>
        /// <returns></returns>
        protected Type ChenkT()
        {
            var type = typeof(T);
            if (type.IsArray)
            {
                throw new Exception("勿使用嵌套数组");
            }
            return type;
        }

        /// <summary>
        /// 验证TOther且返回类型
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <returns></returns>
        protected Type ChenkT<TOther>()
        {
            var type = typeof(TOther);
            if (type.IsArray)
            {
                throw new Exception("勿使用嵌套数组");
            }
            return type;
        }

        private DateTime SwitchTime(MemberInfo member)
        {
            var arr = member.ToString().Split(' ');
            if (arr.Length > 1)
            {
                switch (arr[1].ToLower())
                {
                    case "now": return DateTime.Now;
                    case "today": return DateTime.Today;
                    case "maxvalue": return DateTime.MaxValue;
                    case "minvalue": return DateTime.MinValue;
                    case "utcnow": return DateTime.UtcNow;
                }
                throw new NotImplementedException("意料之外的DateTime常量");
            }
            throw new NotImplementedException("意料之外的member数据");
        }
    }
}
