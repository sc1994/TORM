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

            Expression<Func<Model, bool>> exp = x => x.Date == DateTime.Today;
            var info = new StringBuilder();
            Explain.ExplainTool.Explain(exp, info);

            Console.WriteLine(info.ToString());
            Console.ReadLine();
        }
    }

    class Model
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }
    }
}
