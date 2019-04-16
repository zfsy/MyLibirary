using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLibirary
{
    public static class DirPath
    {
        /// <summary>
        /// 获取程序相关的数据文件路径(替换当前库的dll路径)
        /// </summary>
        /// <param name="relativePath">eg. @"../../Docs/"</param>
        /// <returns></returns>
        public static string GetDataDir(string relativePath)
        {
            string exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                                                         + System.IO.Path.DirectorySeparatorChar;
            string gDataDir = new Uri(new Uri(exeDir), relativePath).LocalPath;

            return gDataDir;
        }


    }
}
