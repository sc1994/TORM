using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Explain
{
    public class ExplainInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 字段和值之间的关系
        /// </summary>
        public ExpressionType? Type { get; set; } = null;
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
        public ExpressionType? Prior { get; set; } = null;
    }

    public class ContentWhere : Content
    {
        public ContentWhere()
        {
            Info = new List<ExplainInfo>
            {
                new ExplainInfo()
            };
        }
        public List<ExplainInfo> Info { get; set; }

        private ExplainInfo Last => Info[Info.Count - 1];

        public override void Append(string info)
        {
            Last.Field = info;
        }

        public override void Append(object info)
        {
            Last.Value = info;
            Info.Add(new ExplainInfo());
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

    public class ContetSelect : Content
    {
        public ContetSelect()
        {
            Info = new List<ExplainInfo>
            {
                new ExplainInfo()
            };
        }

        public List<ExplainInfo> Info { get; set; }

        private ExplainInfo Last => Info[Info.Count - 1];

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

    public class Content
    {
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
    }
}
