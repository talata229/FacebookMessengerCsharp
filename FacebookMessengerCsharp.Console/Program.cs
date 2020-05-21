using FacebookMessengerCsharp.Client.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Chay ngon
            //// Run example
            // Basic_Usage.Run().GetAwaiter().GetResult();

            //// Wait for keypress
            //System.Console.ReadKey();
            #endregion


            // Instantiate FBClient
            FBClient_Cookies client = new FBClient_Cookies();

            try
            {
                await client.TryLogin();
            }
            catch
            {
                // Read email and pw from console
                System.Console.WriteLine("Insert Facebook email:");
                var email = System.Console.ReadLine();
                System.Console.WriteLine("Insert Facebook password:");
                var password = System.Console.ReadLine();

                // Login with username and password
                await client.DoLogin(email, password);
            }

            ////await client.StartListening();
            //// Login with username and password
            await client.send(new FB_Message { text = "Chúc 1 ngày tốt lành123456" }, "100005048402622", ThreadType.USER);
            System.Console.WriteLine("Done!");
            System.Console.ReadKey();
        }

        public static Task SendMessageToListUser()
        {
            FBClient_Simple simple = new FBClient_Simple();
            return Task.Factory.StartNew(async () =>
            {
                await simple.send(new FB_Message { text = "Chúc 1 ngày tốt lành123" }, "100005048402622", ThreadType.USER);
                await simple.send(new FB_Message { text = "How are you" }, kwangtran229, ThreadType.USER);
            });
        }
    }
}
