namespace ORM.Interface
{
    public interface IWhere { }

    public interface IWhere<in TFunc, out TResult> where TResult : IWhere
    {
        TResult Where(TFunc exp);
    }
}
