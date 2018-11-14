namespace ORM.Interface
{
    public interface IQueryWhere<T> : IWhere, IWhere<T, IQueryWhere<T>>, IQueryOrder<T>
    {

    }

    public interface IQueryWhere<T, TJoin> : IWhere, IWhere<T, TJoin, IQueryWhere<T, TJoin>>, IQueryOrder<T, TJoin>
    {

    }

    public interface IQueryWhere<T, TJoin1, TJoin2> : IWhere, IWhere<T, TJoin1, TJoin2, IQueryWhere<T, TJoin1, TJoin2>>, IQueryOrder<T, TJoin1, TJoin2>
    {

    }

    public interface IQueryWhere<T, TJoin1, TJoin2, TJoin3> : IWhere, IWhere<T, TJoin1, TJoin2, TJoin3, IQueryWhere<T, TJoin1, TJoin2, TJoin3>>, IQueryOrder<T, TJoin1, TJoin2, TJoin3>
    {

    }

    public interface IQueryWhere<T, TJoin1, TJoin2, TJoin3, TJoin4> : IWhere, IWhere<T, TJoin1, TJoin2, TJoin3, TJoin4, IQueryWhere<T, TJoin1, TJoin2, TJoin3, TJoin4>>, IQueryOrder<T, TJoin1, TJoin2, TJoin3, TJoin4>
    {

    }
}
