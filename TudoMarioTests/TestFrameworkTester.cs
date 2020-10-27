using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TudoMarioTests
{
    [TestClass]
    public class TestFrameworkTester
    {
        [TestMethod]
        public void TestOneEqualsOne()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
