using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace MultiLedController.utils
{
    public class GetMacUtils
    {
        public static string GetMac()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch
            {
                return "00:00:00:00:00:00";
            }
        }
    }
}
