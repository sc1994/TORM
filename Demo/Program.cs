using System;
using System.Linq.Expressions;
using Explain;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ORM.ORM.Query<Model>()
                .Select(x => x.Name, x => x.Date)
                .Where(x => x.Date == DateTime.Today && x.Name1 == "123123")
                .Where(x => x.Name == "123345345" || x.Name2 == "88304")
                .OrderA(x => x.Name1).Exist();

            var info = new ContentWhere();
            Expression<Func<Model, bool>>
                a = x => x.Name == "234" && x.Date == DateTime.Today && x.Name1 == "123";
            Explain.ExplainTool.Explain(a, info);

            Console.ReadLine();
        }
    }

    class Model
    {
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public DateTime Date { get; set; }
    }

    class Model2
    {
        public string Name2 { get; set; }
        public string Name12 { get; set; }
        public string Name22 { get; set; }
        public DateTime Date2 { get; set; }
    }
    class Model3
    {
        public string Name3 { get; set; }
        public string Name13 { get; set; }
        public string Name23 { get; set; }
        public DateTime Date3 { get; set; }
    }
}
