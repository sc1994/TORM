namespace ORM.Interface
{
    public interface IUpdate<in T>
    {
        long Update(Transaction transaction = null);
        long Update(int top, Transaction transaction = null);
        long Update(T model, Transaction transaction = null);
    }
}
