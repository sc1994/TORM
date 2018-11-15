namespace ORM.Interface
{
    public interface IUpdate
    {
        int Update();
        int Update(int top);
    }

    public class UpdateRecord
    {
        public string Field { get; set; }

        public object Old { get; set; }

        public object New { get; set; }
    }
}
