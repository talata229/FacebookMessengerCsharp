﻿using FacebookMessengerCsharp.Client.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.App
{
    public class Basic_Usage_App
    {
        private static readonly AutoResetEvent _closing = new AutoResetEvent(false);

        public static async Task Run()
        {
            // Instantiate FBClient
            MessengerClient client = new FBClient_Cookies_App();

            try
            {
                // Try logging in from saved session
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

            // Start listening for new messages
            await client.StartListening();

            //// Fetch latest threads
            //var threads = await client.fetchThreadList();
            //threads.ForEach(v => Console.WriteLine(v));

            //var info = await client.fetchThreadInfo(new List<string> { threads[0].uid });

            //// Fetch own profile
            //var self = await client.fetchProfile();
            //Console.WriteLine(self);

            //// Fetch users I'm chatting with
            //var users = await client.fetchAllUsers();
            //users.ForEach(v => Console.WriteLine(v));

            //// Find user by name/id
            //var search = await client.searchForUsers("Marco", 2);
            //search.ForEach(v => Console.WriteLine(v));

            //// Fetch latest messages
            //var messages = await client.fetchThreadMessages(threads.FirstOrDefault()?.uid, 5);
            //messages.ForEach(v => Console.WriteLine(v));

            //// Send a message to myself
            //var msg_uid = await client.sendMessage("Message test", thread_id: client.GetUserUid(), thread_type: ThreadType.USER);
            //if (msg_uid != null)
            //{
            //    Console.WriteLine("Message sent: {0}", msg_uid);
            //}

            // Send an emoji to myself
            await client.send(new FB_Message { text = "Chúc 1 ngày tốt lành" }, "100005048402622", ThreadType.USER);
            //await client.sendEmoji("👍", EmojiSize.LARGE, thread_id: client.GetUserUid(), thread_type: ThreadType.USER);

            // Send a local file to myself
            /*
            using (FileStream stream = File.OpenRead(@"C:\Users\Marco\Documents\a032.pdf"))
            {
                //await client.sendLocalImage(@"C:\Users\Marco\Pictures\Saved Pictures\opengraph.png", stream, null, client.GetUserUid(), ThreadType.USER);
                await client.sendLocalFiles(
                    file_paths: new Dictionary<string, Stream>() { { @"C:\Users\Marco\Documents\a032.pdf", stream } },
                    message: null,
                    thread_id: client.GetUserUid(),
                    thread_type: ThreadType.USER);
            }
            */

            // Send a remote image to myself
            //await client.sendRemoteImage(@"https://freeaddon.com/wp-content/uploads/2018/12/cat-memes-25.jpg", thread_id: client.GetUserUid(), thread_type: ThreadType.USER);

            // Stop listening Ctrl+C
            System.Console.WriteLine("Listening... Press Ctrl+C to exit.");
            System.Console.CancelKeyPress += new ConsoleCancelEventHandler((s, e) => { e.Cancel = true; _closing.Set(); });
            _closing.WaitOne();
            client.StopListening();
            // Logging out is not required
            // await client.DoLogout();
        }
    }
}
