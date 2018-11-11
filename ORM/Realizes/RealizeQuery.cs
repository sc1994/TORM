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
        protected List<Expression> _where = new List<Expression>();
        protected List<(Expression, JoinEnum)> _join = new List<(Expression, JoinEnum)>();
        protected List<Expression> _selects = new List<Expression>();
        protected List<(Expression, string)> _selectAlias = new List<(Expression, string)>();
        protected List<(Expression, OrderEnum)> _orders = new List<(Expression, OrderEnum)>();
        protected List<Expression> _groups = new List<Expression>();
        protected Dictionary<string, object> _params = new Dictionary<string, object>();
        protected List<Type> useTables = new List<Type>
        {
            typeof(T)
        };
        protected List<Type> allTables = new List<Type>();

        public bool Exist()
        {
            var sql = $"SELECT COUNT(1) FROM {GetTable()} {GetJoin()} {GetWhere()};";
            throw new NotImplementedException();
        }

        public T First()
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()};";
            throw new NotImplementedException();
        }

        public TOther First<TOther>()
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()};";
            throw new NotImplementedException();
        }

        public IEnumerable<TOther> Find<TOther>()
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()};";
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find()
        {
            var sql = $"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()};";
            throw new NotImplementedException();
        }

        public (IEnumerable<T> data, int total) Page(int index, int size)
        {
            var sql = new StringBuilder($"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()};");
            ToPage(index, size, sql);
            throw new NotImplementedException();
        }

        public (IEnumerable<TOther> data, int total) Page<TOther>(int index, int size)
        {
            var sql = new StringBuilder($"{GetSelect()} \r\nFROM {GetTable()} {GetJoin()} {GetWhere()};");
            ToPage(index, size, sql);
            throw new NotImplementedException();
        }

        private StringBuilder GetWhere()
        {
            var result = new StringBuilder("\r\nWHERE 1=1");
            foreach (var item in _where)
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

        private StringBuilder GetJoin()
        {
            var result = new StringBuilder();

            _join.ForEach(x =>
            {
                var c = new ContentJoin();
                ExplainTool.Explain(x.Item1, c);
                c.Rinse();
                // 收集全部表
                allTables.AddRange(c.Info.Select(s => s.Table));
                allTables.AddRange(c.Info.Select(s => s.Table2));

                foreach (var info in c.Info)
                {
                    string param;
                    var type = info.Type.ToExplain();
                    if (info.Value == null
                        && (type == "=" || type == "<>")
                        && info.Table2 == null
                        && string.IsNullOrWhiteSpace(info.Field2)) // 当不是和表字段的比较，且 == null 或者 != null时，采取SQL 语法
                    {
                        param = "null";
                        type = type == "=" ? "IS" : "IS NOT";
                    }
                    else
                    {
                        param = $"@{info.Table.Name}_{info.Field}_{_params.Count}";
                        _params.Add(param, info.Value);
                    }

                    if (info.Table2 != null && !string.IsNullOrWhiteSpace(info.Field2))
                    {
                        result.Append($"\r\n  {x.Item2.ToExplain()} {GetJoinTable()} ON {info.Table.Name}.{info.Field} {type} {info.Table2.Name}.{info.Field2}");
                        // 收集已用表
                        useTables.Add(info.Table);
                        useTables.Add(info.Table2);
                    }
                    else
                    {
                        result.Append($"\r\n  {info.Prior.ToExplain()} {info.Table.Name}.{info.Field} {type} {param}");
                    }
                }
            });
            return result;
        }

        private StringBuilder GetGroup()
        {
            var result = new StringBuilder("\r\nGROUP BY");

            _groups.ForEach(item =>
            {
                var c = new ContentEasy();
                ExplainTool.Explain(item, c);
                c.Rinse();
                foreach (var info in c.Info)
                {
                    result.Append($"\r\n  {info.Table.Name}.{info.Field},");
                }
            });
            return result.Remove(result.Length - 1, 1);
        }

        private StringBuilder GetOrder()
        {
            var result = new StringBuilder("\r\nORDER BY");

            _orders.ForEach(item =>
            {
                var c = new ContentEasy();
                ExplainTool.Explain(item.Item1, c);
                c.Rinse();
                foreach (var info in c.Info)
                {
                    result.Append($"\r\n  {info.Table.Name}.{info.Field} {item.Item2.ToExplain()}");
                }
            });
            return result.Remove(result.Length - 1, 1);
        }

        /// <summary>
        /// 获取需要join 的表（取全部表，取已用表，未用过的就是需要join 的表）
        /// todo 这样的计算方式应该不能满足多外键的情况，需要考虑别的办法
        /// </summary>
        /// <returns></returns>
        private string GetJoinTable()
        {
            var all = allTables.Where(x => x != null).Select(x => x.Name).Distinct();
            var use = useTables.Where(x => x != null).Select(x => x.Name).Distinct();
            var flag = all.Except(use);
            if (flag.Count() == 1)
            {
                return flag.FirstOrDefault();
            }
            throw new Exception("获取join表失败！");
        }

        /// <summary>
        /// 取主表
        /// </summary>
        /// <returns></returns>
        private string GetTable()
        {
            return typeof(T).Name;
        }

        private void ToPage(int index, int size, StringBuilder sql)
        {
            // todo 不同数据库的分页有细微差距
        }
    }
}
