using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management;
using System.Text.RegularExpressions;

namespace MyLibirary
{
    class DeviceOpera
    {
        #region UsbDevice

        public static bool IsExistUsbDevice(UInt16 VendorID, UInt16 ProductID)
        {
            bool isExist = false;
            string querySql = "SELECT * FROM Win32_USBControllerDevice";
            string matchExpres = "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}";

            ManagementObjectCollection usbList = new ManagementObjectSearcher(querySql).Get();
            if (usbList != null)
            {
                foreach (ManagementObject usbObj in usbList)
                {
                    // 获取设备实体的DeviceID
                    String Dependent = (usbObj["Dependent"] as String).Split(new Char[] { '=' })[1];
                    Match match = Regex.Match(Dependent, matchExpres);
                    if (match.Success)
                    {
                        UInt16 theVendorID =
                                        Convert.ToUInt16(match.Value.Substring(4, 4), 16);   // 供应商标识

                        UInt16 theProductID =
                                        Convert.ToUInt16(match.Value.Substring(13, 4), 16); // 产品编号

                        if (theVendorID == VendorID && theProductID == ProductID)
                        {
                            isExist = true;
                            break;
                        }
                    }
                }
            }

            return isExist;
        }
        #endregion
    }
}
