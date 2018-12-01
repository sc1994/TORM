using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class MethodTest : BaseTest
    {
        [TestMethod]
        public void JoinAndTest()
        {
            var result0 = TORM.Query<rules, schedules>()
                              .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                              .JoinL((r, s) => r.schedule_id == s.id && r.id > 1)
                              .Find<view>();
            Debug.Assert(result0.Any());
            Parallel.Invoke(() =>
                            {
                                var result1 = TORM.Query<rules, schedules>()
                                                  .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                  .JoinL((r, s) => r.schedule_id == s.id && r.id > 1)
                                                  .Find<view>();
                                Debug.Assert(result1.Any());
                            },
                            () =>
                            {
                                var result2 = TORM.Query<rules, schedules>()
                                                  .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                  .JoinR((r, s) => r.schedule_id == s.id && r.id > 1)
                                                  .Find<view>();
                                Debug.Assert(result2.Any());
                            },
                            () =>
                            {
                                var result3 = TORM.Query<rules, schedules>()
                                                  .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                  .Join((r, s) => r.schedule_id == s.id && r.id > 1)
                                                  .Find<view>();
                                Debug.Assert(result3.Any());
                            }, () =>
                               {
                                   var result1 = TORM.Query<rules, schedules>()
                                                     .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                     .JoinL((r, s) => r.schedule_id == s.id && r.id > 1)
                                                     .Find<view>();
                                   Debug.Assert(result1.Any());
                               },
                            () =>
                            {
                                var result2 = TORM.Query<rules, schedules>()
                                                  .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                  .JoinR((r, s) => r.schedule_id == s.id && r.id > 1)
                                                  .Find<view>();
                                Debug.Assert(result2.Any());
                            },
                            () =>
                            {
                                var result3 = TORM.Query<rules, schedules>()
                                                  .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                  .Join((r, s) => r.schedule_id == s.id && r.id > 1)
                                                  .Find<view>();
                                Debug.Assert(result3.Any());
                            }, () =>
                               {
                                   var result1 = TORM.Query<rules, schedules>()
                                                     .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                     .JoinL((r, s) => r.schedule_id == s.id && r.id > 1)
                                                     .Find<view>();
                                   Debug.Assert(result1.Any());
                               },
                            () =>
                            {
                                var result2 = TORM.Query<rules, schedules>()
                                                  .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                  .JoinR((r, s) => r.schedule_id == s.id && r.id > 1)
                                                  .Find<view>();
                                Debug.Assert(result2.Any());
                            },
                            () =>
                            {
                                var result3 = TORM.Query<rules, schedules>()
                                                  .Select(((r, s) => r.id, "r_id"), ((r, s) => s.id, "s_id"))
                                                  .Join((r, s) => r.schedule_id == s.id && r.id > 1)
                                                  .Find<view>();
                                Debug.Assert(result3.Any());
                            });
            Thread.Sleep(10000);
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
    }
}
