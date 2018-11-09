using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Expression<Func<Model, bool>> exp = x =>

            //    (x.Date == DateTime.Now || x.Name == "1") && x.Name2 == "3" && (x.Date == DateTime.Now || x.Name == "1" || x.Name2 == "3");

            var a = new List<string> { "1", "2", "3" };
            var b = "4";

            Expression<Func<Model, bool>> exp = x => x.Name.In(a);
            Expression<Func<Model, bool>> exp2 = x => x.Name == b;

            //Expression<Func<Model, bool>> exp2 = x => x.Name == string.Join(",", a.Where(y => Convert.ToInt32(y) > 1));

            var info = new StringBuilder();
            Explain.ExplainTool.Explain(exp, info);
            var info2 = new StringBuilder();
            Explain.ExplainTool.Explain(exp2, info2);

            Console.WriteLine(info.ToString());
            Console.WriteLine(info2.ToString());
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
