using Newtonsoft.Json;
using ORM;
using StackExchange.Redis;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("start");
            //TORM.AutoTable<SqlLog>();
            //var config = "118.24.27.231:6379,password=sun940622";
            //var conn = ConnectionMultiplexer.Connect(config);
            //var sub = conn.GetSubscriber();
            //sub.Subscribe("LogSql",
            //              (channel, message) =>
            //              {
            //                  Console.WriteLine(message);
            //                  var info = JsonConvert.DeserializeObject<SqlLog>(message);
            //                  TORM.Insert(info);
            //              });
            //while (true)
            //{
            //    Console.WriteLine("pong");
            //    Thread.Sleep(6000);
            //}

            //var path = @"C:\Users\Administrator\Desktop\Untitled-1.txt";
            //var lines = File.ReadAllText(path).Split("\r\n");
            //var count = 0;
            //foreach (var line in lines)
            //{
            //    var sa = line.Split(new[] { "\\\"QuestionType\\\":" }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var s in sa)
            //    {
            //        if (s.Contains("\\\"QuestionId\\\":"))
            //        {
            //            var type = s.Split("\\\"")[0];
            //            var qid = s.Split("\\\"QuestionId\\\":")[1].Split("\\\"")[0];
            //            var pkey = s.Split("\\\"PostKey\\\":")[1].Split("\\\"")[1];
            //            File.AppendAllLines(@"D:/4.txt", new[] { type + qid + pkey + "|" });
            //            count++;
            //        }
            //    }
            //}

            //Console.WriteLine(count);
            var coun = 0;

            var lines = File.ReadAllLines(@"D:/all.txt");
            lines = lines.Select(x => x.Trim()).Distinct().ToArray();
            var g = lines.GroupBy(x => x.Split(',')[0] + "," + x.Split(',')[2]);
            foreach (var item in g)
            {
                if (item.Count() > 1)
                {
                    //Console.WriteLine($"{item.Key}++++{item.Count()}");
                    foreach (var v in item.Skip(1))
                    {

                        File.AppendAllLines("D:/5.txt", new []{ v });
                        coun++;
                    }
                    File.AppendAllLines("D:/5.txt", new[] { "-------------" });
                }

            }

            Console.WriteLine(coun);

            Console.ReadLine();


        }
    }

    [Table("Log", DBTypeEnum.MySQL)]
    class SqlLog
    {
        [Key, Identity]
        public long Id { get; set; }
        [Field(Length: 5120)]
        public string SqlStr { get; set; }
        [Field(Length: 5120)]
        public string Param { get; set; }
        [Field(Length: 5120)]
        public string StackTrace { get; set; }
        public DateTime EndTime { get; set; }
        [Field(Precision: 11)]
        public double ExplainSpan { get; set; }
        [Field(Precision: 11)]
        public double ConnectSpan { get; set; }
        [Field(Precision: 11)]
        public double ExecuteSpan { get; set; }
    }
}
