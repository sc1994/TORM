using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System.Diagnostics;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class MethodTest : BaseTest
    {
        [TestMethod]
        public void JoinAndTest()
        {
            var result1 = TORM.Query<Rules, Schedules>()
                              .Select(((r, s) => r.Id, "RId"), ((r, s) => s.Id, "SId"))
                              .JoinL((r, s) => r.ScheduleId == s.Id && r.Id > 1)
                              .Find<View>();
            Debug.Assert(result1.Any());
        }

        [TestMethod]
        public void GroupTest()
        {
            var result = TORM.Query<Rules>()
                             .Select(x => x.ScheduleId)
                             .Group(x => x.ScheduleId)
                             .Find();
            Debug.Assert(result.Any());
        }

        [TestMethod]
        public void HavingTest()
        {
            var result = TORM.Query<Rules>()
                             .Select(x => x.ScheduleId)
                             .Group(x => x.ScheduleId)
                             .Having(x => x.ScheduleId >= 0)
                             .Find();
            Debug.Assert(result.Any());
        }

        [TestMethod]
        public void WhereTest()
        {
            var result = TORM.Query<Rules>()
                             .Where(x => x.Id > 1)
                             .Find();
            Debug.Assert(result.Any());
        }
    }
}
