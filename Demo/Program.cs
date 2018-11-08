using System;
using System.Linq.Expressions;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var name = "123";
            Expression<Func<Model, bool>> exp = x => x.Name == "123";

            Explain.ExplainTool.Explain(exp);

            Console.ReadLine();
        }
    }

    class Model
    {
        public string Name { get; set; }
    }
}
