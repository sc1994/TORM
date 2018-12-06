using System;

namespace ORM.Interface
{
    /// <summary>
    /// 实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUpdateWhere<T> : 
        IWhere, 
        IWhere<Func<T, bool>, IUpdateWhere<T>>, 
        IUpdate<T>
    {
        
    }
}