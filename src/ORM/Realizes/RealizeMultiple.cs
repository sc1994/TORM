using ORM.Interface.IQuery;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ORM.Realizes
{
    /// <summary>
    /// 多查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeMultiple<T> : RealizeCommon<T>, IMultiple<T>
    {
        /// <summary>
        /// 一对多
        /// </summary>
        /// <typeparam name="TForeign"></typeparam>
        /// <param name="limit"></param>
        /// <returns></returns>
        public (T main, IEnumerable<TForeign> foreign) First<TForeign>(long limit)
        {
            var sql = new StringBuilder($"{GetSelect()}\r\nFROM {GetTableName()}{GetWhere()}{GetGroup()}{GetHaving()}{GetOrder()} LIMIT 1;");
            var table = ChenkT();
            var f = ChenkT<TForeign>();
            foreach (var item in table.GetProperties())
            {
                var foreign = item.GetCustomAttributes(typeof(ForeignAttribute), true).FirstOrDefault();
                if (foreign != null && foreign is ForeignAttribute fValue && fValue.Table.Name == f.Name)
                {
                    var fTable = GetTableInfo(f);
                    sql.AppendLine($"SELECT * FROM {fTable.Name} WHERE {fValue.Foreign} = @Key;");
                }
            }
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 一对多
        /// </summary>
        /// <typeparam name="TForeign1"></typeparam>
        /// <typeparam name="TForeign2"></typeparam>
        /// <param name="limit1"></param>
        /// <param name="limit2"></param>
        /// <returns></returns>
        public (T main, IEnumerable<TForeign1> foreign1, IEnumerable<TForeign2> foreign2) First<TForeign1, TForeign2>(long limit1, long limit2)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取 select sql 代码
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetSelect()
        {
            return GetSliceSql(SqlTypeEnum.Select, () =>
            {
                var result = new StringBuilder("SELECT");
                _selects.ForEach(x => ToSelect(x, null, result));
                _selectAlias.ForEach(x => ToSelect(x.Item1, x.Item2, result));
                if (result.ToString() != "SELECT")
                {
                    result.SafeRemove(result.Length - 1, 1);
                    var table = GetTableInfo();
                    var key = table.Key ?? table.Identity;
                    result.Append($"\r\n  {table.Name}.{key.Name} INTO @Key");
                }
                else
                {
                    result.Append("\r\n  *");
                }
                return result;
            });
        }
    }
}
