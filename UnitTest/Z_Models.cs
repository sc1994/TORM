using ORM;
using System;

namespace UnitTest
{
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
}
