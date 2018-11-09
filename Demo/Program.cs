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
            // todo 目前的结构不能支持括号改变优先级的写法。可以通过改变api的结构控制执行的优先级
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
                Console.Write($" {item.Prior} {item.Field} {item.Type} Value");
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
