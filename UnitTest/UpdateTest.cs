using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class UpdateTest : BaseTest
    {
        [TestMethod]
        public void UpdateAllTest()
        {
            var result = TORM.Update<Rules>()
                             .Set(x => x.CreatedAt, DateTime.Now)
                             .Where(x => x.Id > 1)
                             .Update();
            Debug.Assert(result > 3);
        }

        [TestMethod]
        public void UpdateTopTest()
        {
            var result = TORM.Update<Rules>()
                             .Set(x => x.DeletedAt, DateTime.Now)
                             .Where(x => x.Id > 1)
                             .Update(3);
            Debug.Assert(result > 0 && result < 4);
        }

        [TestMethod]
        public void UpdateModelTest()
        {
            var result = TORM.Update(new Rules
            {
                Id = 2,
                DeletedAt = DateTime.Today,
                CreatedAt = DateTime.Today,
                RuleDate = DateTime.Today,
                ScheduleId = 999,
                Type = 888,
                UpdatedAt = DateTime.Now
            });
            Debug.Assert(result == 1);
        }
    }
}
