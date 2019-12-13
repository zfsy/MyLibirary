using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyLibirary;

namespace MyLibiraryTests
{
    [TestClass]
    public class UnitTestTimeOpera
    {
        [TestMethod]
        public void TestFirstDayOfMonth()
        {
            DateTime result = TimeOpera.FirstDayOfMonth(DateTime.Now);
            Assert.AreEqual("", result.ToString());
        }
    }
}
