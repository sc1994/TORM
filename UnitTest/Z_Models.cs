using ORM;
using System;
using System.Diagnostics;
using System.Threading;

namespace UnitTest
{
    public class BaseTest
    {
        public BaseTest()
        {
            TORM.Debug = true;
        }

        ~BaseTest()
        {
            //Thread.Sleep(20000); todo 延迟整个数据的回收
        }
    }

    [Table("tally", DBTypeEnum.MySQL, "rules")]
    public class rules
    {
        [Key, Identity, Field(NotNull: false)]
        public long id { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;
        public DateTime deleted_at { get; set; } = DateTime.Now;
        public long schedule_id { get; set; }
        public int type { get; set; }
        public DateTime rule_date { get; set; } = DateTime.Now;
    }

    [Table("tally", DBTypeEnum.MySQL)]
    public class schedules
    {
        [Key, Identity]
        public long id { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;
        public DateTime deleted_at { get; set; } = DateTime.Now;
        public string content { get; set; }
    }

    public class view
    {
        public long r_id { get; set; }
        public long s_id { get; set; }
    }
}
