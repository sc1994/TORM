using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System;
using System.Diagnostics;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class QueryTest
    {
        [TestMethod]
        public void FindTest()
        {
            var result = TORM.Query<rules>()
                             .Where(x => x.id > 0)
                             .Find();
            Debug.Assert(result.Any());
        }

        public void FindTop()
        {
            var result = TORM.Query<rules>()
                             .Where(x => x.id > 0)
                             .Find(2);
            Debug.Assert(result.Count() == 2);
        }
    }

    [Table("tally", DBTypeEnum.MySQL, "rules")]
    class rules
    {
        [Key, Identity, Field(NotNull: false)]
        public long id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public long schedule_id { get; set; }
        public int type { get; set; }
        public DateTime rule_date { get; set; }
    }
}
