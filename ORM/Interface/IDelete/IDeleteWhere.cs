namespace ORM.Interface.IDelete
{
    public interface IDeleteWhere<T> : IWhere, IWhere<T, IQueryWhere<T>>, IDelete
    {

    }
}
