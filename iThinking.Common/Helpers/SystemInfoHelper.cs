using Microsoft.Win32;
using System;
using System.IO;
using System.Management;
using System.Net;
using System.Windows.Forms;

namespace iThinking.Common.Helpers
{
    public static class SystemInfoHelper
    {
        public static string getOSName()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    return managementObject["Caption"].ToString();
                }
            }

            return "Unknown";
        }

        public static string getOSArchitecture()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["OSArchitecture"] != null)
                {
                    return managementObject["OSArchitecture"].ToString();
                }
            }

            return "Unknown";
        }

        public static string getOSServicePack()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["CSDVersion"] != null)
                {
                    return managementObject["CSDVersion"].ToString();
                }
            }

            return "Unknown";
        }

        public static string getProcessorName()
        {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);
            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    return processor_name.GetValue("ProcessorNameString").ToString();
                }
            }

            return "Unknown";
        }

        public static string getMachineName()
        {
            return Environment.MachineName;
        }

        public static string getUserName()
        {
            return Environment.UserName;
        }

        public static bool is64BitOperatingSystem()
        {
            return Environment.Is64BitOperatingSystem;
        }

        public static string getDomainName()
        {
            return Environment.UserDomainName;
        }

        public static string getMonitorSize()
        {
            return SystemInformation.PrimaryMonitorSize.ToString();
        }

        public static string getProcessor()
        {
            RegistryKey rk = Registry.LocalMachine;
            rk = rk.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
            return rk.GetValue("ProcessorNameString").ToString();
        }

        public static string getOsVersion()
        {
            ManagementObjectSearcher myOperativeSystemObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject obj in myOperativeSystemObject.Get())
            {
                return obj["Caption"].ToString();
            }

            return "Unknown";
        }

        /// <summary>
        /// RAM Gb
        /// </summary>
        /// <returns></returns>
        public static long getRAMSize()
        {
            string Query = "SELECT Capacity FROM Win32_PhysicalMemory";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(Query);

            UInt64 Capacity = 0;
            foreach (ManagementObject WniPART in searcher.Get())
            {
                Capacity += Convert.ToUInt64(WniPART.Properties["Capacity"].Value);
            }

            return (long)(Capacity / 1024 / 1024 / 1024);
        }

        /// <summary>
        /// Gb
        /// </summary>
        /// <returns></returns>
        public static long getHDDTotalSize()
        {
            long _totalSize = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.DriveType == DriveType.Fixed)
                {
                    _totalSize += drive.TotalSize;
                }
            }

            return _totalSize / 1024 / 1024 / 1024;
        }

        /// <summary>
        /// Gb
        /// </summary>
        /// <returns></returns>
        public static long getHDDTotalFreeSize()
        {
            long _totalSize = 0;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.DriveType == DriveType.Fixed)
                {
                    _totalSize += drive.TotalFreeSpace;
                }
            }

            return _totalSize / 1024 / 1024 / 1024;
        }

        public static DateTime? getOSInstallDate()
        {
            var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);

            key = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", false);
            if (key != null)
            {
                DateTime startDate = new DateTime(1970, 1, 1, 0, 0, 0);
                object objValue = key.GetValue("InstallDate");
                string stringValue = objValue.ToString();
                Int64 regVal = Convert.ToInt64(stringValue);

                return startDate.AddSeconds(regVal);
            }

            return null;
        }

        public static string getInstalledApps()
        {
            string _installedApps = "";

            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        try
                        {
                            string _appName = sk.GetValue("DisplayName").ToString();

                            if (!string.IsNullOrEmpty(_appName))
                            {
                                if (!string.IsNullOrEmpty(_installedApps))
                                {
                                    _installedApps += " | ";
                                }

                                _installedApps += _appName;
                            }
                        }
                        catch
                        { }
                    }
                }
            }

            return _installedApps;
        }

        public static string getSerialNumber()
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
            foreach (ManagementObject getserial in MOS.Get())
            {
                return getserial["SerialNumber"].ToString();
            }

            return "Unknown";
        }

        public static string getPartNumber()
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
            foreach (ManagementObject getserial in MOS.Get())
            {
                try
                {
                    return getserial["PartNumber"].ToString();
                }
                catch { }
            }

            return "Unknown";
        }

        public static string getMACAddress()
        {
            ManagementClass OmrokDynamicsMAC_MClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection OmrokDynamicsMAC_MOCollection = OmrokDynamicsMAC_MClass.GetInstances();
            string OmrokDynamicsMACAddress = String.Empty;
            foreach (ManagementObject OmrokDynamicsMAC_MObject in OmrokDynamicsMAC_MOCollection)
            {
                if (OmrokDynamicsMACAddress == String.Empty)
                {
                    if ((bool)OmrokDynamicsMAC_MObject["IPEnabled"] == true)
                    {
                        OmrokDynamicsMACAddress = OmrokDynamicsMAC_MObject["MacAddress"].ToString();
                    }
                }
                OmrokDynamicsMAC_MObject.Dispose();
            }
            OmrokDynamicsMACAddress = OmrokDynamicsMACAddress.Replace(":", "");
            return OmrokDynamicsMACAddress;
        }

        public static string getIPAddress()
        {
            string _ipAddress = "";

            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    if (!string.IsNullOrEmpty(_ipAddress))
                    {
                        _ipAddress += " | ";
                    }

                    _ipAddress += ip.ToString();
                }
            }

            if (!string.IsNullOrEmpty(_ipAddress))
                return _ipAddress;
            else
                return "Unknown";
        }
    }
}