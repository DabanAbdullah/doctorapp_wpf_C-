using System;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Win32;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SystemInformationWindow.xaml
    /// </summary>
    public partial class SystemInformationWindow : Window
    {
        public SystemInformationWindow()
        {
            InitializeComponent();
            GetSystemInformations();
        }

        private void SystemInfoWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e != null && e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void MainHeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GetSystemInformations()
        {
            SystemInfoWindowOperatingSystem.Content = GetOsFriendlyName();
            SystemInfoWindowNetFrameworkVersion.Content = Get45Or451FromRegistry();
            SystemInfoWindowWindowsUserName.Content = Environment.UserName;
            SystemInfoWindowDomainName.Content = Environment.UserDomainName;
            SystemInfoWindowRam.Content = GetMemory();
            SystemInfoWindowProcessor.Content = GetProcessor();
            SystemInfoWindowLanIp.Content = GetLanIpAddress();
            SystemInfoWindowWanIp.Content = GetWanIpAddress();
            SystemInfoWindowRubyVersion.Content = GetAssemblyInformation();
        }

        public static string GetOsFriendlyName()
        {
            var searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (var os in searcher.Get())
            {
                return os["Caption"].ToString();
            }
            return string.Empty;
        }

        private static string Get45Or451FromRegistry()
        {
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                var releaseKey = Convert.ToInt32(ndpKey.GetValue("Release"));
                if (true)
                {
                    return CheckFor45DotVersion(releaseKey);
                }
            }
        }

        // Checking the version using >= will enable forward compatibility,   
        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 393273)
            {
                return "4.6 RC or later";
            }
            if ((releaseKey >= 379893))
            {
                return "4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return "4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5 or later";
            }
            // This line should never execute. A non-null release key should mean 
            // that 4.5 or later is installed. 
            //return "No 4.5 or later version detected";
            return "Please install .Net Framework Version 4.5 or later!";
        }

        private static string GetMemory()
        {
            var query = "SELECT MaxCapacity FROM Win32_PhysicalMemoryArray";
            var searcher = new ManagementObjectSearcher(query);
            foreach (var wniPart in searcher.Get())
            {
                UInt32 sizeinKb = Convert.ToUInt32(wniPart.Properties["MaxCapacity"].Value);
                UInt32 sizeinMb = sizeinKb / 1024;
                UInt32 sizeinGb = sizeinMb / 1024;
                //Console.WriteLine("Size in KB: {0}, Size in MB: {1}, Size in GB: {2}", SizeinKB, SizeinMB, SizeinGB);
                return sizeinGb + " GB";
            }

            return string.Empty;
        }

        private static string GetProcessor()
        {
            var query = "SELECT Name FROM Win32_Processor";
            var searcher = new ManagementObjectSearcher(query);
            foreach (var wniPart in searcher.Get())
            {
                return wniPart.Properties["Name"].Value.ToString();
            }

            return string.Empty;
        }

        private static string GetLanIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }

        private static string GetWanIpAddress()
        {
            using (var webClient = new WebClient())
            {
                string wanIpAddress;

                try
                {
                    wanIpAddress = IPAddress.Parse(webClient.DownloadString("http://tools.feron.it/php/ip.php")).ToString();
                }
                catch (Exception)
                {
                    wanIpAddress = "Bitte besuchen Sie die Seite www.wieistmeineip.ch";
                }

                return wanIpAddress;
            }
        }

        private static string GetAssemblyInformation()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
