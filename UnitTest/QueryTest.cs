using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System.Diagnostics;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class QueryTest
    {
        public QueryTest()
        {
            TORM.Debug = true;
        }

        [TestMethod]
        public void CountTest()
        {
            var result = TORM.Query<rules>()
                             .Where(x => x.id > 0)
                             .Count();
            Debug.Assert(result > 3);
        }

        [TestMethod]
        public void FindTest()
        {
            var result = TORM.Query<rules>()
                             .Where(x => x.id > 0)
                             .Find();
            Debug.Assert(result.Any());
        }

        [TestMethod]
        public void FindTopTest()
        {
            var result = TORM.Query<rules>()
                             .Where(x => x.id > 0)
                             .Find(2);
            Debug.Assert(result.Count() == 2);
        }

        [TestMethod]
        public void PageTest()
        {
            var (data, total) = TORM.Query<rules>()
                                    .Select(x => x.id, x => x.created_at, x => x.deleted_at)
                                    .Where(x => x.id > 0 && x.schedule_id > 0 && x.type > 0)
                                    .OrderA(x => x.id)
                                    .OrderD(x => x.created_at)
                                    .Page(1, 3);

            Debug.Assert(data.Count() > 3 && total > 3);
        }
    }
}
