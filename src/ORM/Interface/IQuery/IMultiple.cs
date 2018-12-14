using System.Collections.Generic;

namespace ORM.Interface
{
    /// <summary>
    /// todo 支持外键查询，当多表之间存在一对多，一对一，多对一关系时。主动查询数据
    /// </summary>
    public partial interface IQuery<T>
    {
        /// <summary>
        /// 外键
        /// </summary>
        /// <returns></returns>
        (T main, IEnumerable<TForeign> foreign) Foreign<TForeign>(long limit = 0);
    }
}
