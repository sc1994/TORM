using ORM.Interface.IQuery;

namespace ORM.Realizes
{
    public class RealizeMultiple<T> : RealizeCommon<T>, IMultiple<T>
    {
        public T First<TForeign>(long limit)
        {
            
        }
    }
}
