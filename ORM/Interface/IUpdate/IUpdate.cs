namespace ORM.Interface
{
    public interface IUpdate<in T>
    {
        int Update(Transaction transaction = null);
        int Update(int top, Transaction transaction = null);
        int Update(T model, Transaction transaction = null);
    }
}
