using System;
using System.Linq.Expressions;
using System.Text;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Expression<Func<Model, bool>> exp = x =>

                (x.Date == DateTime.Now || x.Name == "1") && x.Name2 == "3" && (x.Date == DateTime.Now || x.Name == "1" || x.Name2 == "3");

            var info = new StringBuilder();
            Explain.ExplainTool.Explain(exp, info);

            Console.WriteLine(info.ToString());
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
}
