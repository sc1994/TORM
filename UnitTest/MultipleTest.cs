using Microsoft.VisualStudio.TestTools.UnitTesting;
using ORM;
using System.Diagnostics;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class MultipleTest : BaseTest
    {
        [TestMethod]
        public void Test()
        {
            var result = TORM.Multiple<City>().First<Town>();
            Debug.Assert(result.foreign.Any());
        }
    }
}
