using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace ModuleWellIncideGraph.Extension
{
    /// <summary>
    /// XML序列号、反序列化、节点等操作辅助类
    /// </summary>
    public class XmlHelper
    {
        #region 变量
        protected string strXmlFile;

        protected XmlDocument objXmlDoc = new XmlDocument();
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="XmlFile">XML文件路径</param>
        public XmlHelper(string XmlFile)
        {
            try
            {
                objXmlDoc.Load(XmlFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        #region 静态方法

        /// <summary>
        /// XML序列化
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="obj">对象实例</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static bool XmlSerialize(string path, object obj, Type type)
        {
            try
            {
                if (!File.Exists(path))
                {
                    FileInfo fi = new FileInfo(path);
                    if (!fi.Directory.Exists)
                    {
                        Directory.CreateDirectory(fi.Directory.FullName);
                    }
                }

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer format = new XmlSerializer(type);

                    format.Serialize(stream, obj, ns);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// XML反序列化
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object XmlDeserialize(string path, Type type)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer formatter = new XmlSerializer(type);
                    stream.Seek(0, SeekOrigin.Begin);
                    object obj = formatter.Deserialize(stream);
                    return obj;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 公用方法
        /// <summary>
        /// 获取指定节点下面的XML子节点
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <returns></returns>
        public XmlNodeList Read(string XmlPathNode)
        {
            try
            {
                XmlNode xn = objXmlDoc.SelectSingleNode(XmlPathNode);
                return xn.ChildNodes;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 读取节点属性内容
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <param name="Attrib">节点属性</param>
        /// <returns></returns>
        public string Read(string XmlPathNode, string Attrib)
        {
            string value = "";
            try
            {
                XmlNode xn = objXmlDoc.SelectSingleNode(XmlPathNode);
                value = (Attrib.Equals("") ? xn.InnerText : xn.Attributes[Attrib].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// 获取元素节点对象
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <param name="elementName">元素节点名称</param>
        /// <returns></returns>
        public XmlElement GetElement(string XmlPathNode, string elementName)
        {
            XmlElement result = null;

            XmlNode nls = objXmlDoc.SelectSingleNode(XmlPathNode);
            foreach (XmlNode xn1 in nls)    //遍历
            {
                XmlElement xe2 = (XmlElement)xn1;   // 转换类型
                if (xe2.Name == elementName)
                {
                    result = xe2;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取元素节点的值
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <param name="elementName">元素节点名称</param>
        /// <returns></returns>
        public string GetElementData(string XmlPathNode, string elementName)
        {
            string result = null;

            XmlNode nls = objXmlDoc.SelectSingleNode(XmlPathNode);

            foreach (XmlNode xn1 in nls)
            {
                XmlElement xe2 = (XmlElement)xn1;
                if (xe2.Name == elementName)
                {
                    result = xe2.InnerText;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 替换某节点的内容
        /// </summary>
        /// <param name="XmlPathNode">XML节点</param>
        /// <param name="Content">节点内容</param>
        public void Replace(string XmlPathNode, string Content)
        {
            objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="Node">节点</param>
        public void Delete(string Node)
        {
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            objXmlDoc.SelectSingleNode(mainNode).RemoveChild(objXmlDoc.SelectSingleNode(Node));
        }

        /// <summary>
        /// 保存XML文档
        /// </summary>
        public void Save()
        {
            try
            {
                objXmlDoc.Save(strXmlFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            objXmlDoc = null;
        }
        #endregion
    }
}
