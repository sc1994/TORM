namespace ORM.Interface
{
    public interface IUpdateWhere<T> : IWhere, IWhere<T, IUpdateWhere<T>>, IUpdate
    {

    }
}