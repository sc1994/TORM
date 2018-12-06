using System;

namespace ORM.Interface.IDelete
{
    /// <summary>
    /// IDeleteWhere
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeleteWhere<T> : 
        IWhere, 
        IWhere<Func<T, bool>, IDeleteWhere<T>>, 
        IDelete<T>
    {

    }
}
