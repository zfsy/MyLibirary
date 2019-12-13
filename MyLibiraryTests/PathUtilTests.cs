using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using MyLibirary;

namespace MyLibiraryTests
{
    [TestClass]
    public class PathUtilTests
    {
        [TestMethod]
        public void CheckInvalidPath_Test()
        {
            string invalidPath = "dfdfdfd>dfdsfa";
            Assert.IsFalse(DirPath.IsValidPath(invalidPath));           
        }

        [TestMethod]
        public void CheckValidPath_Test()
        {
            string validPath = @"D:\Codes\UkeyMaker\UkeyMaker";
            Assert.IsFalse(DirPath.IsValidPath(validPath));
        }
    }
}
