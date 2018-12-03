using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class DeleteTest : BaseTest
    {
        [TestMethod]
        public void DeleteByIdTest()
        {
            var id = TORM.Query<Rules>()
                         .Select(x => x.Id)
                         .Where(x => x.Id > 10)
                         .OrderD(x => x.Id)
                         .First<long>();
            var result = TORM.Delete<Rules, long>(id);
            Debug.Assert(result == 1);
        }

        [TestMethod]
        public void DeleteWhereTest()
        {
            var id = TORM.Query<Rules>()
                         .Select(x => x.Id)
                         .First<long>();
            var result = TORM.Delete<Rules>()
                             .Where(x => x.Id == id)
                             .Delete();
            Debug.Assert(result == 1);
        }
    }
}
