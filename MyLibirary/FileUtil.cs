using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace MyLibirary
{
    class FileUtil
    {
        #region Stream、byte[]和文件之间的转换
        /// <summary>
        /// 将流读取到缓冲区中
        /// </summary>
        /// <param name="stream">原始流</param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            try
            {
                // 创建缓冲区
                byte[] buffer = new byte[stream.Length];

                // 读取流
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));

                // 返回流
                return buffer;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将Stream写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        public static void StreamToFile(Stream stream, string fileName)
        { 
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Stream FileToStream(string fileName)
        {
            FileStream fileStream =
                            new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将文件读取到缓冲区中
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns></returns>
        public static byte[] FileToBytes(string filePath)
        {
            int fileSize = GetFileSize(filePath);

            byte[] buffer = new byte[fileSize];

            FileInfo fi = new FileInfo(filePath);
            FileStream fs = fi.Open(FileMode.Open);

            try
            {
                fs.Read(buffer, 0, fileSize);

                return buffer;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
            }
        }

        public static string FileToString(string filePath)
        {
            return FileToString(filePath, Encoding.Default);
        }

        /// <summary>
        /// 将文件读取到字符串中
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string FileToString(string filePath, Encoding encoding)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {               
                throw ex;
            }
        }

        /// <summary>
        /// 获取一个文件的长度，单位为Byte
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns></returns>
        public static int GetFileSize(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            return (int)fi.Length;
        }
        #endregion
    }
}
