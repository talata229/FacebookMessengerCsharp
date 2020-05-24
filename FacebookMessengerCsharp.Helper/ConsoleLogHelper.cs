using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Helper
{
    public class ConsoleLogHelper
    {
        public static void WriteToConsole(string text)
        {
        Console.WriteLine($"{DateTime.Now.ToString("hh:mm:ss")}: {text}");
        }
    }
}
