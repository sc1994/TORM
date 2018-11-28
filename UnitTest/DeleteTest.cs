using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class DeleteTest
    {
        public DeleteTest()
        {
            TORM.Debug = true;
        }

        [TestMethod]
        public void DeleteByIdTest()
        {
            var id = TORM.Query<rules>()
                         .Select(x => x.id)
                         .Where(x => x.id > 10)
                         .OrderD(x => x.id)
                         .First<long>();
            var result = TORM.Delete<rules, long>(id);
            Debug.Assert(result == 1);
        }

        [TestMethod]
        public void DeleteWhereTest()
        {
            var id = TORM.Query<rules>()
                         .Select(x => x.id)
                         .First<long>();
            var result = TORM.Delete<rules>()
                             .Where(x => x.id == id)
                             .Delete();
            Debug.Assert(result == 1);
        }
    }
}
