using FacebookMessengerCsharp.Client.API;
using FacebookMessengerCsharp.Helper;
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

            await client.SendRandomMessageToListUser(ListHelper.ListUser, ListHelper.ListMessage);

            System.Console.WriteLine("Done!");
            System.Console.ReadKey();
        }
    }
}
