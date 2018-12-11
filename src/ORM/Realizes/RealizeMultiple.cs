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
        public (T main, IEnumerable<TForeign> foreign) First<TForeign>(long limit)
        {
            var sql = new StringBuilder($"{GetSelect()}\r\nFROM {GetTableName()}{GetWhere()} LIMIT 1;");
            var table = ChenkT();
            var f = ChenkT<TForeign>();
            foreach (var item in table.GetProperties())
            {
                var foreign = item.GetCustomAttributes(typeof(ForeignAttribute), true).FirstOrDefault();
                if (foreign != null)
                {
                    if(foreign is ForeignAttribute fValue)
                    {
                        
                    }
                }
            }
            sql.AppendLine($"");
            throw new System.NotImplementedException();
        }

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
