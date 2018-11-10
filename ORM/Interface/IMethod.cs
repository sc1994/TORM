using System.Collections.Generic;

namespace ORM.Interface
{
    public interface IMethod<T>
    {
        bool Exist();
        T First();
        TOther First<TOther>();
        IEnumerable<TOther> Find<TOther>();
        IEnumerable<T> Find();
        (IEnumerable<T> data, int total) Page(int index, int size);
        (IEnumerable<TOther> data, int total) Page<TOther>(int index, int size);
    }
}
