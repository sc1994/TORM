using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System.Diagnostics;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class QueryTest : BaseTest
    {
        [TestMethod]
        public void CountTest()
        {
            var result = TORM.Query<Rules>()
                             .Where(x => x.Id > 0)
                             .Count();
            Debug.Assert(result > 3);
        }

        [TestMethod]
        public void FindTest()
        {
            var result = TORM.Query<Rules>()
                             .Where(x => x.Id > 0)
                             .Find();
            Debug.Assert(result.Any());
        }

        [TestMethod]
        public void FindTopTest()
        {
            var result = TORM.Query<Rules>()
                             .Where(x => x.Id > 0)
                             .Find(2);
            Debug.Assert(result.Count() == 2);
        }

        [TestMethod]
        public void PageTest()
        {
            var (data, total) = TORM.Query<Rules>()
                                    .Select(x => x.Id, x => x.CreatedAt, x => x.DeletedAt)
                                    .Where(x => x.Id > 0 && x.ScheduleId >= 0 && x.Type >= 0)
                                    .OrderA(x => x.Id)
                                    .OrderD(x => x.CreatedAt)
                                    .Page(1, 3);

            Debug.Assert(data.Any() && total > 0);
        }
    }
}
