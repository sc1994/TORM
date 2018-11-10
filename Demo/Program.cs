﻿using Explain;
using System;
using System.Linq.Expressions;
using ORM;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Expression<Func<Model, bool>> exp = x =>

                x.Date == DateTime.Now || x.Name == "1" && x.Name2 == "3" && x.Date == DateTime.Now || x.Name == "1" || x.Name2 == "3";
            // todo 目前的结构不能支持括号改变优先级的写法。可以通过改变api的结构控制执行的优先级
            var b = DateTime.Now;

            //Expression<Func<Model, bool>> exp = x => x.Name.In(new List<string> { "1", "2", "3" }.Where(w => Convert.ToInt32(w) > 1));
            //Expression<Func<Model, bool>> exp2 = x => x.Name == b;

            //Expression<Func<Model, bool>> exp2 = x => x.Name == string.Join(",", a.Where(y => Convert.ToInt32(y) > 1));

            var info = new ContetSelect();
            //ExplainTool.Explain(exp, info);

            Expression<Func<Model, object[]>> exp2 = x => new object[] { x.Name, x.Date, ORMTool.Max(x.Name1) };
            Explain.ExplainTool.Explain(exp2, info);

            if (info is ContetSelect)
            {
                foreach (var item in info.Info)
                {
                    Console.Write($"{item.Field} {item.Method}");
                }
            }
            else if (info is ContentWhere)
            {
                foreach (var item in info.Info)
                {
                    Console.Write($"{item.Prior} {item.Field} {item.Type}{item.Method} Value ");
                }
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
