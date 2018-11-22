using System.Collections.Generic;

namespace ORM.Interface
{
    public interface IInsert<in T>
    {
        int Insert(T models);
        int InsertBatch(IEnumerator<T> models);
        int InsertBatch(params T[] models);
    }
}
