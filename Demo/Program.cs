﻿using Explain;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ORM.ORM.Query<Model>()
                .Select(x => x.Name, x => x.Date, x => ORMTool.Max(x.Name1))
                .Select(x => new object[] { x.Name, x.Date, ORMTool.Max(x.Name1) })
                .Select(x => x.Name1, "name")
                .Select((x => x.Name2, "name2"), (x => x.Name, "name3"))
                .Where(x => x.Name.StartsWith("1"))
                .Where(x => x.Name.EndsWith("2"))
                .Where(x => x.Name.Contains("3"))
                .Where(x => x.Date == DateTime.Today)
                .Where(x => x.Date > DateTime.Today)
                .Where(x => x.Date >= DateTime.Today)
                .Where(x => x.Date <= DateTime.Today)
                .Where(x => x.Date < DateTime.Today)
                .Where(x => x.Date != DateTime.Today)
                .Where(x => x.Name1 == null)
                .Where(x => x.Name1 != null)
                .Where(x => x.Name.In(new[] { "", "" }.ToList()))
                .Where(x => x.Name.NotIn(new[] { "", "" }.ToList()))
                //.OrderA(x => x.Name1)
                .First();

            var info = new ContentWhere();
            Expression<Func<Model, bool>>
                a = x => x.Name == "234" && x.Date == DateTime.Today && x.Name1 == "123";
            ExplainTool.Explain(a, info);

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
