using Explain;
using System;
using System.Linq.Expressions;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Expression<Func<Model, bool>> exp = x =>

                (x.Date == DateTime.Now || x.Name == "1") && x.Name2 == "3" && (x.Date == DateTime.Now || x.Name == "1" || x.Name2 == "3");

            var b = DateTime.Now;

            //Expression<Func<Model, bool>> exp = x => x.Name.In(new List<string> { "1", "2", "3" }.Where(w => Convert.ToInt32(w) > 1));
            //Expression<Func<Model, bool>> exp2 = x => x.Name == b;

            //Expression<Func<Model, bool>> exp2 = x => x.Name == string.Join(",", a.Where(y => Convert.ToInt32(y) > 1));

            var info = new Content();
            ExplainTool.Explain(exp, info);
            //Explain.ExplainTool.Explain(exp2, info2);
            Console.Write("WHERE 1=1");
            foreach (var item in info.Info)
            {
                Console.Write($" {item.Prior} {string.Join("", item.Lb)}{item.Field} {item.Type} Value{string.Join("", item.Rb)}");
            }
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
