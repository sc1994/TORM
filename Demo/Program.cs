using Newtonsoft.Json;
using ORM;
using StackExchange.Redis;
using System;
using System.Threading;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");
            TORM.AutoTable<SqlLog>();
            var config = "118.24.27.231:6379,password=sun940622";
            var conn = ConnectionMultiplexer.Connect(config);
            var sub = conn.GetSubscriber();
            sub.Subscribe("LogSql",
                          (channel, message) =>
                          {
                              Console.WriteLine(message);
                              var info = JsonConvert.DeserializeObject<SqlLog>(message);
                              TORM.Insert(info);
                          });
            while (true)
            {
                Console.WriteLine("keep live");
                Thread.Sleep(6000);
            }
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
