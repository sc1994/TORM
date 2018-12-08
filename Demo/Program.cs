using ORM;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var type = typeof(City);

            foreach (var property in type.GetProperties())
            {
                
            }

            while (true)
            {
                Console.WriteLine("keep live");
                Thread.Sleep(6000);
            }
        }
    }

    [Table("Test", DBTypeEnum.MySQL)]
    class Province
    {
        [Key, Identity, Field(Comment: "省份主键")]
        public long Id { get; set; }
        public string Name { get; set; }
        [Foreign(typeof(City), "ProvinceId")]
        public List<City> Citys { get; set; }
    }

    [Table("Test", DBTypeEnum.MySQL)]
    class City
    {
        [Key, Identity]
        public long Id { get; set; }
        public string Name { get; set; }
        [Foreign(typeof(Town), "CityId")]
        public List<Town> Towns { get; set; }
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
