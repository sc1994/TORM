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
            var result = TORM.Delete<rules, long>(19L);
            Debug.Assert(result == 1);
        }

        [TestMethod]
        public void DeleteWhereTest()
        {
            var result = TORM.Delete<rules>()
                             .Where(x => x.id == 18)
                             .Delete();
            Debug.Assert(result == 1);
        }
    }
}
