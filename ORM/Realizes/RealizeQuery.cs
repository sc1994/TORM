using ORM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Explain;

namespace ORM.Realizes
{
    public class RealizeQuery<T> : IQuery<T>
    {
        protected List<Expression> _ands = new List<Expression>();
        protected List<Expression> _selects = new List<Expression>();
        protected List<(Expression<Func<T, object>>, string)> _selectAlias = new List<(Expression<Func<T, object>>, string)>();
        protected List<Expression> _orderDs = new List<Expression>();
        protected List<Expression> _orderAs = new List<Expression>();
        protected List<Expression> _groups = new List<Expression>();
        protected Dictionary<string, object> _params = new Dictionary<string, object>();

        public bool Exist()
        {
            var sql = $"SELECT COUNT(1) FROM TEST.Model {GetWhere()};";
            throw new NotImplementedException();
        }

        public T First()
        {
            var sql = $"{GetSelect()} \r\nFROM TEST.Model {GetWhere()};";
            throw new NotImplementedException();
        }

        public TOther First<TOther>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TOther> Find<TOther>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find()
        {
            throw new NotImplementedException();
        }

        public (IEnumerable<T> data, int total) Page(int index, int size)
        {
            throw new NotImplementedException();
        }

        public (IEnumerable<TOther> data, int total) Page<TOther>(int index, int size)
        {
            throw new NotImplementedException();
        }


        private StringBuilder GetWhere()
        {
            var result = new StringBuilder("\r\nWHERE 1=1");
            foreach (var item in _ands)
            {
                var c = new ContentWhere();
                ExplainTool.Explain(item, c);
                c.Rinse();
                result.Append("\r\nAND(");
                c.Info.ForEach(x => ToWhere(x, result));
                result.Append("\r\n)");
            }
            return result;
        }

        private void ToWhere(ExplainInfo info, StringBuilder result)
        {
            string param;
            var type = info.Type.ToExplain();
            if (info.Value == null && (type == "=" || type == "<>"))
            {
                param = "null";
                type = type == "=" ? "IS" : "IS NOT";
            }
            else
            {
                param = $"@{info.Table.Name}_{info.Field}_{_params.Count}";
                _params.Add(param, info.Value);
            }

            var ex = info.Prior.ToExplain();
            var sql = $"\r\n  {(ex == null ? "" : ex + " ")}{info.Table.Name}.{info.Field} ";
            if (ExplainTool.Methods.Contains(info.Method))
            {
                if (info.Method == "Contains")
                {
                    sql += $"LIKE '%'+{param}+'%'";
                }
                else if (info.Method == "StartsWith")
                {
                    sql += $"LIKE {param}+'%'";
                }
                else if (info.Method == "EndsWith")
                {
                    sql += $"LIKE '%'+{param}";
                }
                else if (info.Method == "In")
                {
                    sql += $"IN ({param})";
                }
                else if (info.Method == "NotIn")
                {
                    sql += $"NOT IN ({param})";
                }
            }
            else
            {
                sql += $"{type} {param}";
            }
            result.Append(sql);
        }

        private StringBuilder GetSelect()
        {
            var result = new StringBuilder("SELECT");
            _selects.ForEach(x => ToSelect(x, null, result));
            _selectAlias.ForEach(x => ToSelect(x.Item1, x.Item2, result));
            if (result.ToString() != "SELECT")
            {
                result.Remove(result.Length - 1, 1);
            }
            else
            {
                result.Append(" *");
            }
            return result;
        }

        private void ToSelect(Expression item, string alias, StringBuilder result)
        {
            var c = new ContentEasy();
            ExplainTool.Explain(item, c);
            c.Rinse();
            c.Info.ForEach(x =>
            {
                var a = string.IsNullOrWhiteSpace(alias) ? "" : $" AS {alias}";
                if (string.IsNullOrWhiteSpace(x.Method))
                {
                    result.Append($"\r\n  {x.Table.Name}.{x.Field}{a},");
                }
                else
                {
                    result.Append($"\r\n  {x.Method.ToUpper()}({x.Table.Name}.{x.Field}){a},");
                }
            });

        }
    }
}
