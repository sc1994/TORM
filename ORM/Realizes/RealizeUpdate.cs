using ORM.Interface;

namespace ORM.Realizes
{
    /// <summary>
    /// 解析 更新 相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RealizeUpdate<T> : RealizeToSql<T>, IUpdate
    {
        public int Update()
        {
            var sql = $"UPDATE {GetTableName()}{GetSet()}{GetWhere()}";
            throw new System.NotImplementedException();
        }

        public int Update(int top)
        {
            var sql = ToTop(top);
            sql = string.Format(sql, $"{GetSet()}{GetWhere()}");
            throw new System.NotImplementedException();
        }
    }
}
