namespace ORM.Interface
{
    public interface IUpdate<in T>
    {
        int Update();
        int Update(int top);
        int Update(T model);
    }
}
