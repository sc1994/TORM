namespace ORM.Interface
{
    public interface IQueryOrder<T, in TFunc> : IQuery<T>
    {
        IQueryOrder<T, TFunc> OrderA(TFunc exp);
        IQueryOrder<T, TFunc> OrderD(TFunc exp);
        IQueryOrder<T, TFunc> OrderA(params TFunc[] exps);
        IQueryOrder<T, TFunc> OrderD(params TFunc[] exps);
    }
}
