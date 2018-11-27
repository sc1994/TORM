using System.Collections.Generic;

namespace ORM.Interface
{
    public interface IInsert<in T>
    {
        long Insert(T model, Transaction transaction = null);
        long InsertBatch(IEnumerator<T> models, Transaction transaction = null);
    }
}
