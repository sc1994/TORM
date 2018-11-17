using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Explain
{
    public class ExplainInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public Type Table { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 表名(join或者复杂查询会在一个表达式节点遇到两个表)
        /// </summary>
        public Type Table2 { get; set; }
        /// <summary>
        /// 字段名(join或者复杂查询会在一个表达式节点遇到两个表)
        /// </summary>
        public string Field2 { get; set; }
        /// <summary>
        /// 字段和值之间的关系
        /// </summary>
        public ExpressionType? Type { get; set; }
        /// <summary>
        /// 字段和值之间的方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 两个树节点之间的关系
        /// </summary>
        public ExpressionType? Prior { get; set; }
        /// <summary>
        /// 是否是new array （解决在表达式中直接实例数组）
        /// </summary>
        public bool IsNewArray { get; set; }
    }

    /// <summary>
    /// where 的内容
    /// </summary>
    public class ContentWhere : Content
    {
        public override void Append(string info)
        {
            Last.Field = info;
        }

        public override void Append(object info)
        {
            if (Last.IsNewArray)
            {
                _newArray.Add(info);
            }
            else
            {
                Last.Value = info;
            }

            if (!string.IsNullOrWhiteSpace(Last.Field) &&
                !string.IsNullOrWhiteSpace(Last.Method) &&
                !Last.IsNewArray)
            {
                Info.Add(new ExplainInfo());
            }
        }

        public override void Append(MethodInfo method)
        {
            Last.Method = method.Name;
        }

        public override void Append(ExpressionType type)
        {
            if (type == ExpressionType.OrElse || type == ExpressionType.AndAlso)
            {
                Last.Prior = type;
            }
            Last.Type = type;
        }
    }

    /// <summary>
    /// 简单内容（select，order，group）
    /// </summary>
    public class ContentEasy : Content
    {
        public override void Append(string info)
        {
            Last.Field = info;
            Info.Add(new ExplainInfo());
        }

        public override void Append(MethodInfo method)
        {
            Info[Info.Count - 2].Method = method.Name;
        }
    }

    /// <summary>
    /// join
    /// </summary>
    public class ContentJoin : ContentWhere
    {
        public override void Append(string info)
        {
            if (string.IsNullOrWhiteSpace(Last.Field))
            {
                Last.Field = info;
            }
            else
            {
                Last.Field2 = info;
                Info.Add(new ExplainInfo());
            }
        }

        public override void Append(Type info)
        {
            if (Last.Table == null)
            {
                Last.Table = info;
            }
            else
            {
                Last.Table2 = info;
            }
        }
    }

    /// <summary>
    /// 内容基类
    /// </summary>
    public class Content
    {
        public Content()
        {
            Info = new List<ExplainInfo>
            {
                new ExplainInfo()
            };
        }

        public List<ExplainInfo> Info { get; set; }

        protected ExplainInfo Last => Info[Info.Count - 1];

        public virtual void Append(string info)
        {

        }

        public virtual void Append(object info)
        {

        }

        public virtual void Append(MethodInfo method)
        {

        }

        public virtual void Append(ExpressionType type)
        {

        }

        public virtual void Append(Type info)
        {
            Last.Table = info;
        }

        /// <summary>
        /// 解决在表达式中直接实例数组
        /// </summary>
        protected readonly List<object> _newArray = new List<object>();

        /// <summary>
        /// 是否直接在表达式中实例数组
        /// </summary>
        public virtual void IsNewArray()
        {
            Last.IsNewArray = true;
        }

        /// <summary>
        /// 结束实例数组的时机
        /// </summary>
        public virtual void OverNewArray()
        {
            Last.Value = _newArray;
            Info.Add(new ExplainInfo());
        }

        public void Rinse()
        {
            Info.RemoveAll(x => string.IsNullOrEmpty(x.Field));
        }
    }
}
