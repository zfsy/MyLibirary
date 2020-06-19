using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;

namespace MyLibirary
{
    public class AppUtil
    {
        public static List<SOFTWARE> GetAllSoftWareList()
        {
            List<SOFTWARE> softwareList = new List<SOFTWARE>();
            SOFTWARE soft = null;

            RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            if (key != null)
            {
                foreach (String SubkeyName in key.GetSubKeyNames())
                {
                    RegistryKey Subkey = key.OpenSubKey(SubkeyName);
                    if (Subkey != null)
                    {
                        string SoftwareName = Subkey.GetValue("DisplayName", "Nothing").ToString();
                        string DisplayVersion = Subkey.GetValue("DisplayVersion", "Nothing").ToString();
                        if (SoftwareName != "Nothing")
                        {
                            soft = new SOFTWARE(SoftwareName, DisplayVersion);
                            softwareList.Add(soft);
                        }
                    }
                }
            }

            return softwareList;
        }

        public static bool IsExitInSystem(List<SOFTWARE> softList, string appName, out SOFTWARE soft)
        {
            soft = null;
            foreach (SOFTWARE software in softList)
            {
                if (software.DisplayName.Contains(appName))
                {
                    soft = software;
                    return true;
                }
            }
            return false;
        }
    }

    public class SOFTWARE
    {
        public string DisplayName;
        public string DisplayVersion;

        public SOFTWARE(string displayName, string displayVersion)
        {
            this.DisplayName = displayName;
            this.DisplayVersion = displayVersion;
        }
    };
}
