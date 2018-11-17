using System;

namespace ORM.Interface.IDelete
{
    public interface IDeleteWhere<T> : IWhere, IWhere<Func<T, bool>, IDeleteWhere<T>>, IDelete<T>
    {

    }
}
