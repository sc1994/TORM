using ORM;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Demo
{
    class Program
    {
        private static int Count { get; set; }
        static void Main(string[] args)
        {
            TORM.AutoTable<Province>();


            Console.WriteLine("OVER");
            Console.ReadLine();
        }
    }

    [Table("tally", DBTypeEnum.MySQL)]
    class Province
    {
        [Key, Identity, Field(Comment: "省份主键")]
        public long Id { get; set; }
        public string Name { get; set; }
        [Foreign("ProvinceId")]
        public List<City> Citys { get; set; }
    }

    [Table("tally", DBTypeEnum.MySQL)]
    class City
    {
        [Key, Identity]
        public long Id { get; set; }
        public string Name { get; set; }
        [Foreign("CityId")]
        public List<Town> Towns { get; set; }
        public long ProvinceId { get; set; }

    }

    [Table("tally", DBTypeEnum.MySQL)]
    class Town
    {
        [Key, Identity]
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
    }
}
