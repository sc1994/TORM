using System.Collections.Generic;

namespace ORM.Interface.IQuery
{
    /// <summary>
    /// todo 支持外键查询，当多表之间存在一对多，一对一，多对一关系时。主动查询数据
    /// </summary>
    public interface IMultiple<T>
    {
        /// <summary>
        /// 查询一条
        /// </summary>
        /// <returns></returns>
        (T main, IEnumerable<TForeign> foreign) First<TForeign>(long limit);

        /// <summary>
        /// 查询一条
        /// </summary>
        /// <returns></returns>
        (T main, IEnumerable<TForeign1> foreign1, IEnumerable<TForeign2> foreign2) First<TForeign1, TForeign2>(long limit1, long limit2);
    }
}
