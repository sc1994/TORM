namespace ORM.Interface.IDelete
{
    public interface IDelete<in T>
    {
        long Delete(Transaction transaction = null);

        long Delete(int top, Transaction transaction = null);

        long Delete<TKey>(TKey key, Transaction transaction = null);
    }
}
