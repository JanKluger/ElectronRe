using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronNET.API;

namespace Test.Services
{
    public static class MessagingService
    {
        public static BrowserWindow MainBrowserWindow { get; set; }

        public static void Send(string channel, string data)
        {
            Console.WriteLine($"Sending {channel}");
            Electron.IpcMain.Send(MainBrowserWindow, channel, data);
        }

        public static void Subscribe(string channel, Action<string> handler)
        {
            Electron.IpcMain.On(channel, (data) =>
            {
                Console.WriteLine($"Receiving {channel}");
                handler(data.ToString());
            });
        }
    }
}