using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleWellIncideGraph.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyLibirary;
using System.Xml;

namespace ModuleWellIncideGraph.Extension.Tests
{
    [TestClass()]
    public class XmlHelperTests
    {
        string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                            + System.IO.Path.DirectorySeparatorChar;
        [TestMethod()]
        public void ReadNodeList_Test()
        {
            string configPath = 
                    new Uri(new Uri(exeDir), @"../../XMLProcess/Configure/booksort.xml").LocalPath;
            XmlHelper helper = new XmlHelper(configPath);
            XmlNodeList nodeList = helper.Read("/bookstore/book");

            Assert.AreEqual(3, nodeList.Count);
        }

        [TestMethod()]
        public void ReadNodeAttribute_Test()
        {
            string configPath =
                    new Uri(new Uri(exeDir), @"../../XMLProcess/Configure/booksort.xml").LocalPath;
            XmlHelper helper = new XmlHelper(configPath);
            string attrib = helper.Read("/bookstore/book", "publicationdate");

            Assert.AreEqual("1997", attrib);
        }

        [TestMethod]
        public void GetElement_Test()
        {
            string configPath =
                new Uri(new Uri(exeDir), @"../../XMLProcess/Configure/booksort.xml").LocalPath;
            XmlHelper helper = new XmlHelper(configPath);
            XmlElement element = helper.GetElement("/bookstore/book", "title");

            Assert.AreEqual("Pride And Prejudice", element.InnerText);
        }


        [TestMethod]
        public void GetElementData_Test()
        {
            string configPath =
                        new Uri(new Uri(exeDir), @"../../XMLProcess/Configure/booksort.xml").LocalPath;
            XmlHelper helper = new XmlHelper(configPath);
            string elementData = helper.GetElementData("/bookstore/book", "title");

            Assert.AreEqual("Pride And Prejudice", elementData);
        }
    }
}