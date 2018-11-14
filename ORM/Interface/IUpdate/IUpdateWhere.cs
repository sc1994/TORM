namespace ORM.Interface
{
    public interface IUpdateWhere<T> : IWhere, IWhere<T, IUpdateWhere<T>>, IUpdate<T>
    {

    }

    //: IWhere<T, IQueryWhere<T>>, IQueryOrder<T>
}