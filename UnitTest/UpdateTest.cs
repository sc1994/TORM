using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class UpdateTest
    {
        public UpdateTest()
        {
            TORM.Debug = true;
        }

        [TestMethod]
        public void UpdateAllTest()
        {
            var result = TORM.Update<rules>()
                             .Set(x => x.created_at, DateTime.Now)
                             .Where(x => x.id > 1)
                             .Update();
            Debug.Assert(result > 3);
        }

        [TestMethod]
        public void UpdateTopTest()
        {
            var result = TORM.Update<rules>()
                             .Set(x => x.deleted_at, DateTime.Now)
                             .Where(x => x.id > 1)
                             .Update(3);
            Debug.Assert(result > 0 && result < 4);
        }

        [TestMethod]
        public void UpdateModelTest()
        {
            var result = TORM.Update(new rules
            {
                id = 2,
                deleted_at = DateTime.Today,
                created_at = DateTime.Today,
                rule_date = DateTime.Today,
                schedule_id = 999,
                type = 888,
                updated_at = DateTime.Now
            });
            Debug.Assert(result == 1);
        }
    }
}
