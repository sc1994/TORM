using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Explain
{
    public class ExplainInfo
    {
        public string Field { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public ExpressionType Prior { get; set; }
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

        private ExplainInfo last => Info[Info.Count - 1];

        public void Append(string info)
        {
            last.Field = info;
        }

        public void Append(object info)
        {
            last.Value = info;
            Info.Add(new ExplainInfo());
        }

        public void Append(MethodInfo method)
        {
            last.Type = method.Name;
        }

        public void Append(ExpressionType type)
        {
            if (type == ExpressionType.OrElse || type == ExpressionType.AndAlso)
            {
                last.Prior = type;
            }
            last.Type = type.ToString();
        }
    }
}
