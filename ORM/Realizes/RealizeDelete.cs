using ORM.Interface.IDelete;

namespace ORM.Realizes
{
    public class RealizeDelete<T> : RealizeToSql<T>, IDelete
    {
        public int Delete()
        {

            throw new System.NotImplementedException();
        }

        public int Delete(int top)
        {
            throw new System.NotImplementedException();
        }
    }
}
