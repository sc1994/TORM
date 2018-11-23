using System.Collections.Generic;

namespace ORM.Interface
{
    public interface IInsert<in T>
    {
        int Insert(T model, Transaction transaction = null);
        int InsertBatch(IEnumerator<T> models, Transaction transaction = null);
    }
}
