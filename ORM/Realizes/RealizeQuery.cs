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
        protected List<Expression<Func<T, bool>>> _ands = new List<Expression<Func<T, bool>>>();
        protected List<Expression<Func<T, bool>>> _ors = new List<Expression<Func<T, bool>>>();
        protected List<Expression<Func<T, object[]>>> _selectss = new List<Expression<Func<T, object[]>>>();
        protected List<Expression<Func<T, object>>> _selects = new List<Expression<Func<T, object>>>();
        protected List<(Expression<Func<T, object>>, string)> _selectAlias = new List<(Expression<Func<T, object>>, string)>();
        protected List<Expression<Func<T, object[]>>> _orderDss = new List<Expression<Func<T, object[]>>>();
        protected List<Expression<Func<T, object>>> _orderDs = new List<Expression<Func<T, object>>>();
        protected List<Expression<Func<T, object[]>>> _orderAss = new List<Expression<Func<T, object[]>>>();
        protected List<Expression<Func<T, object>>> _orderAs = new List<Expression<Func<T, object>>>();
        protected List<Expression<Func<T, object[]>>> _groupss = new List<Expression<Func<T, object[]>>>();
        protected List<Expression<Func<T, object>>> _groups = new List<Expression<Func<T, object>>>();
        protected Dictionary<string, object> _params = new Dictionary<string, object>();

        public bool Exist()
        {
            var sql = $"SELECT COUNT(1) FROM TEST.TEST WHERE 1=1{GetWhere()}";
            throw new NotImplementedException();
        }

        public T First()
        {
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
            var result = new StringBuilder();
            foreach (var item in _ands)
            {
                var c = new ContentWhere();
                ExplainTool.Explain(item, c);
                c.Rinse();
                result.Append("\r\nAND(");
                c.Info.ForEach(x => ToWhere(x, result));
                result.Append("\r\n)");
            }

            result.Append(";");
            return result;
        }

        private void ToWhere(ExplainInfo info, StringBuilder result)
        {
            var param = $"@{info.Table.Name}_{info.Field}_{_params.Count}";
            _params.Add(param, info.Value);
            var ex = info.Prior.ToExplain();
            var sql = $"\r\n  {(ex == null ? "" : ex + " ")}{info.Table.Name}.{info.Field} ";
            if (Const.Methods.Contains(info.Method))
            {
                if (info.Method == "LikeF")
                {
                    sql += $"LIKE '%'+{param}+'%'";
                }
                else if (info.Method == "LikeR")
                {
                    sql += $"LIKE {param}+'%'";
                }
                else if (info.Method == "LikeL")
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
                sql += $"{info.Type.ToExplain()} {param}";
            }
            result.Append(sql);
        }
    }
}
