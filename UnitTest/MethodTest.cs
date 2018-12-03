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
            var result1 = TORM.Query<rules, schedules>()
                              .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                              .JoinL((r, s) => r.schedule_id == s.id && r.id > 1)
                              .Find<view>();
            Debug.Assert(result1.Any());
        }

        [TestMethod]
        public void GroupTest()
        {
            var result = TORM.Query<rules>()
                             .Select(x => x.schedule_id)
                             .Group(x => x.schedule_id)
                             .Find();
            Debug.Assert(result.Any());
        }

        [TestMethod]
        public void HavingTest()
        {
            var result = TORM.Query<rules>()
                             .Select(x => x.schedule_id)
                             .Group(x => x.schedule_id)
                             .Having(x => x.schedule_id > 0)
                             .Find();
            Debug.Assert(result.Any());
        }

        [TestMethod]
        public void WhereTest()
        {
            var result = TORM.Query<rules>()
                             .Where(x => x.id > 1)
                             .Find();
            Debug.Assert(result.Any());
        }
    }
}
