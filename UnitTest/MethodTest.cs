using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System.Diagnostics;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class MethodTest
    {
        public MethodTest()
        {
            TORM.Debug = true;
        }

        [TestMethod]
        public void JoinAndTest()
        {
            var result = TORM.Query<rules, schedules>()
                             .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                             .JoinL((r, s) => r.id == s.id && r.id > 4)
                             .Find<view>();
            Debug.Assert(result.Count() > 3);
        }

        [TestMethod]
        public void GroupTest()
        {
            var result = TORM.Query<rules>()
                             .Select(x => x.schedule_id)
                             .Group(x => x.schedule_id)
                             .Find();
            Debug.Assert(result.Count() == 3);
        }

        [TestMethod]
        public void HavingTest()
        {
            var result = TORM.Query<rules>()
                             .Select(x => x.schedule_id)
                             .Group(x => x.schedule_id)
                             .Having(x => x.schedule_id > 100)
                             .Find();
            Debug.Assert(result.Count() == 1);
        }
    }
}
