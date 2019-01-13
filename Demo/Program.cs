using Newtonsoft.Json;
using ORM;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            File.Delete("D:/1.log");

            var a = new[] { 1L, 2L, 3L, 4L }.ToList();
            var b = 0L;
            var c = DateTime.Now;

            var d = new City { };

            Expression<Func<City, bool>> exp1 = x => x.Date.AddDays(5) == d.Date.AddDays(3);

            var (sql, param) = new Visitor().Translate(exp1);

            Console.WriteLine("WHERE 1 = 1 \r\nAND   " + sql); // .TrimEnd(typeof(City).Name.ToArray())
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(param, Formatting.Indented));

            //while (true)
            //{
            //    Console.WriteLine("keep live");
            //    Thread.Sleep(6000);
            //}
            Console.ReadLine();

            
        }

        [Table("Test", DBTypeEnum.MySQL)]
        public class City
        {
            [Key, Identity]
            public long Id { get; set; }
            public string Name { get; set; }
            public long ProvinceId { get; set; }
            public DateTime Date { get; set; }
        }

        [Table("Test", DBTypeEnum.MySQL)]
        public class Town
        {
            [Key, Identity]
            public long Id { get; set; }
            public string Name { get; set; }
            public long CityId { get; set; }
        }
    }

    [Table("Test", DBTypeEnum.MySQL)]
    class Province
    {
        [Key, Identity, Field(Comment: "省份主键")]
        public long Id { get; set; }
        public string Name { get; set; }
    }

    [Table("Test", DBTypeEnum.MySQL)]
    class City
    {
        [Key, Identity]
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
    }

    [Table("Test", DBTypeEnum.MySQL)]
    class Town
    {
        [Key, Identity]
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
    }
}
