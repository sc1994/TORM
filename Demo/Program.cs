using Dapper;
using MySql.Data.MySqlClient;
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

            var sql =
@"SET @ForeignKey := 0;
SELECT
  City.Name,
  @ForeignKey := City.Id AS _
FROM City
WHERE
  1=1
LIMIT 1;

SELECT
  *
FROM Town 
WHERE
  CityId = @ForeignKey;";

            var sql1 = @"select * from City;select * from Town;";
            using (var connection = new MySqlConnection("server=118.24.27.231;database=Test;uid=root;pwd=sun940622;Allow User Variables=True;"))
            {

                //var read1 = connection.QueryMultiple(sql1);
                var read = connection.QueryMultiple(sql);
                var c = read.ReadFirstOrDefault<City>();
            }


            while (true)
            {
                Console.WriteLine("keep live");
                Thread.Sleep(6000);
            }
        }

        [Table("Test", DBTypeEnum.MySQL)]
        public class City
        {
            [Key, Identity]
            public long Id { get; set; }
            public string Name { get; set; }
            public long ProvinceId { get; set; }
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
