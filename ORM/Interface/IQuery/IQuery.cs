using System.Collections.Generic;

namespace ORM.Interface
{
    public interface IQuery<T>
    {
        bool Exist();
        T First();
        TOther First<TOther>();
        IEnumerable<TOther> Find<TOther>();
        IEnumerable<T> Find();
        IEnumerable<TOther> Find<TOther>(int top);
        IEnumerable<T> Find(int top);
        (IEnumerable<T> data, int total) Page(int index, int size);
        (IEnumerable<TOther> data, int total) Page<TOther>(int index, int size);
    }
}
