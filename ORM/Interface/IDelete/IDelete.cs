namespace ORM.Interface.IDelete
{
    public interface IDelete<in T>
    {
        int Delete();

        int Delete(int top);

        int Delete(T model);
    }
}
