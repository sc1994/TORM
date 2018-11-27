using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace Demo
{
    class Program
    {
        private static int Count { get; set; }
        static void Main(string[] args)
        {
            TORM.AutoTable<Province>();
            TORM.AutoTable<City>();

            //TORM.Insert(new Province
            //{
            //    Name = "江苏"
            //});
            //TORM.Insert(new City
            //{
            //    Name = "淮安",
            //    ProvinceId = 1
            //});
            //TORM.Insert(new City
            //{
            //    Name = "苏州",
            //    ProvinceId = 1
            //});

            using (var con = new MySqlConnection("server=118.24.27.231;database=tally;uid=root;pwd=sun940622;"))
            {
                var result = con.QueryMultiple("select * from Province;select * from City;");
                var province = result.ReadFirstOrDefault<Province>();
                var citys = result.Read<City>();
                province.Citys = citys.ToList();
            }




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
