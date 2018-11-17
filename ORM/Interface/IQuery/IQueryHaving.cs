namespace ORM.Interface.IQuery
{
    public interface IQueryHaving<T, TFunc, in TFuncBool> : IQueryOrder<T, TFunc>
    {
        IQueryHaving<T, TFunc, TFuncBool> Having(TFuncBool exp);
    }
}
