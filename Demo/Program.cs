using ORM;
using System;

namespace Demo
{
    class Program
    {
        private static int Count { get; set; }
        static void Main(string[] args)
        {
            //var e = ORM.ORM.Insert(new rules
            //{
            //    created_at = DateTime.Now,
            //    id = 13,
            //    schedule_id = 2,
            //    type = 1,
            //    deleted_at = DateTime.Now,
            //    rule_date = DateTime.Now,
            //    updated_at = DateTime.Now
            //});

            ORM.ORM.Debug = true;

            var find = ORM.ORM.Query<rules, schedules>()
                          .Select((x, y) => new object[] { x.created_at, x.deleted_at, x.id, x.schedule_id, y.content })
                          .JoinL((x, y) => x.schedule_id == y.id)
                          .Where((x, y) => x.id > 0 && y.id > 0)
                          .OrderD((x, y) => x.id)
                          .Find<rulesView>(3);

            foreach (var item in find)
            {
                Console.WriteLine($"id:{item.id}--created_at:{item.created_at}--schedule_id:{item.schedule_id}--content:{item.content}");
            }



            //var list = new List<int>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    list.Add(i);
            //}
            //var spans = new ConcurrentQueue<TimeSpan>();
            //Parallel.ForEach(list,
            //                 i =>
            //                 {
            //                     var s = DateTime.Now;
            //                     var tr = Transaction.Start();

            //                     var c = ORM.ORM.Update<rules>()
            //                                .Set(x => x.created_at, DateTime.Now)
            //                                .Update(tr);

            //                     var c2 = ORM.ORM.Update<rules>()
            //                                 .Set(x => x.deleted_at, DateTime.Now)
            //                                 .Update(tr);

            //                     Console.WriteLine(i);

            //                     if (i % 2 == 0)
            //                     {
            //                         tr.Commit();
            //                     }
            //                     else
            //                     {
            //                         tr.Rollback();
            //                     }
            //                     spans.Enqueue(DateTime.Now - s);
            //                 });

            //double millisecond = 0;
            //while (!spans.IsEmpty)
            //{
            //    if (spans.TryDequeue(out TimeSpan span))
            //    {
            //        millisecond += span.TotalMilliseconds;
            //    }
            //}

            //ORM.ORM.Query<rules>().Select(x => x.created_at).Find();

            //Console.WriteLine("Hello World!---->" + millisecond + "<--毫秒");




            //ORM.ORM.Query<Model>()
            //    //.Select(x => x.Name)
            //    //.Select(x => x.Name, x => x.Date, x => ORMTool.Max(x.Name1))
            //    .Select(x => new object[]
            //                 {
            //                     x.Name,
            //                     x.Date,
            //                     ORMTool.Max(x.Name1)
            //                 })
            //    //.Select(x => x.Name1, "name")
            //    //.Select((x => x.Name2, "name2"), (x => x.Name, "name3"))
            //    //.Where(x => x.Name.StartsWith("1") && x.Name.EndsWith("2") && x.Name.Contains("3"))
            //    //.Group(x => x.Name)
            //    //.Having(x => x.Name == "1")
            //    //.OrderA(x => x.Name1)
            //    .Find()
            //    ;

            //ORM.ORM.Query<Model, Model3>()
            //    .Select((x, y) => x.Name, (x, y) => y.Date3)
            //    .Join((x, y) => x.Name == y.Name3 && x.Name == "1")
            //    .Exist();

            //ORM.ORM.Update<Model>().Set(x => x.Name, "1").Where(x => x.Date == DateTime.Now).Update();

            //var info = new ContentWhere();
            //Expression<Func<Model, bool>>
            //    a = x => x.Name == "234" && x.Date == DateTime.Today && x.Name1 == "123";
            //ExplainTool.Explain(a, info);

            Console.ReadLine();
        }
    }



    [Table("tally", DBTypeEnum.MySQL, "rules")]
    class rules
    {
        [Key, Identity, Field(NotNull: false)]
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public long schedule_id { get; set; }
        public int type { get; set; }
        public DateTime rule_date { get; set; }
    }

    class rulesView
    {
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public long schedule_id { get; set; }
        public int type { get; set; }
        public DateTime rule_date { get; set; }
        public string content { get; set; }
    }

    [Table("tally", DBTypeEnum.MySQL)]
    class schedules
    {
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public string content { get; set; }
    }

    [Table("test", DBTypeEnum.MySQL, "ModelTable")]
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
