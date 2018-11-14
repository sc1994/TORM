using System.Collections.Generic;

namespace ORM.Interface
{
    public interface IUpdate<T>
    {
        int Update(int top = 0);
        List<UpdateRecord> Update(int top = 0, bool record = false);
    }

    public class UpdateRecord
    {
        public string Field { get; set; }

        public object Old { get; set; }

        public object New { get; set; }
    }
}
