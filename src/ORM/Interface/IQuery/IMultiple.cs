namespace ORM.Interface.IQuery
{
    /// <summary>
    /// todo 支持外键查询，当多表之间存在一对多，一对一，多对一关系时。主动查询数据
    /// </summary>
    public interface IMultiple<out T>
    {
        /// <summary>
        /// 查询一条
        /// </summary>
        /// <returns></returns>
        T First<TForeign>(long limit);
    }
}
