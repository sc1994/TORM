using System;

namespace ORM.Interface
{
    public interface IUpdateWhere<T> : IWhere, IWhere<Func<T, bool>, IUpdateWhere<T>>, IUpdate<T>
    {
        
    }
}