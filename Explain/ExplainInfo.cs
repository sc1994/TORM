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

        private ExplainInfo Last => Info[Info.Count - 1];

        public void Append(string info)
        {
            Last.Field = info;
        }

        public void Append(object info)
        {
            Last.Value = info;
            Info.Add(new ExplainInfo());
        }

        public void Append(MethodInfo method)
        {
            Last.Method = method.Name;
        }

        public void Append(ExpressionType type)
        {
            if (type == ExpressionType.OrElse || type == ExpressionType.AndAlso)
            {
                Last.Prior = type;
            }
            Last.Type = type;
        }
    }
}
