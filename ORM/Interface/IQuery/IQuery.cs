using System.Collections.Generic;

namespace ORM.Interface
{
    public interface IQuery<T>
    {
        bool Exist();
        long Count();
        string CountSql();
        T First();
        string FirstSql();
        TOther First<TOther>();
        IEnumerable<TOther> Find<TOther>();
        IEnumerable<T> Find();
        string FindSql();
        IEnumerable<TOther> Find<TOther>(int top);
        IEnumerable<T> Find(int top);
        string FindSql(int top);
        (IEnumerable<T> data, long total) Page(int index, int size);
        string PageSql(int index, int size);
        (IEnumerable<TOther> data, long total) Page<TOther>(int index, int size);
    }
}
