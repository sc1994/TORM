using ORM;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    public class BaseTest
    {
        public BaseTest()
        {
            TORM.Options(options =>
                         {
                             options.Debug = true; // 调试模式

                             var redis = ConnectionMultiplexer.Connect("118.24.27.231:6379,password=suncheng1994");
                             options.RedisLog = redis; // 使用redis推送sql记录

                             // 配置数据库连接
                             options.DbConfig.Add("Test", "server=118.24.27.231;database=Test;uid=root;pwd=suncheng1994;");
                             options.DbConfig.Add("Log", "server=118.24.27.231;database=Log;uid=root;pwd=suncheng1994;");
                         });

            //TORM.AutoTable<Rules>();
            //TORM.AutoTable<Schedules>();
            //TORM.AutoTable<Province>();
            //TORM.AutoTable<City>();
            //TORM.AutoTable<Town>();
        }
    }

    [Table("Test", DBTypeEnum.MySQL, "Rules")]
    public class Rules
    {
        [Key, Identity, Field(NotNull: false)]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime DeletedAt { get; set; } = DateTime.Now;
        public long ScheduleId { get; set; }
        public int Type { get; set; }
        public DateTime RuleDate { get; set; } = DateTime.Now;
    }

    [Table("Test", DBTypeEnum.MySQL)]
    public class Schedules
    {
        [Key, Identity]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime DeletedAt { get; set; } = DateTime.Now;
        [Field(Length: 2048)]
        public string Content { get; set; }
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

    public class view
    {
        public long r_id { get; set; }
        public long s_id { get; set; }
    }
}
