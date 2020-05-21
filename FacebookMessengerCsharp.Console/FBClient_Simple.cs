﻿using FacebookMessengerCsharp.Client.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace FacebookMessengerCsharp.Console
{
    public class FBClient_Simple : MessengerClient
    {
        public FBClient_Simple()
        {
            this.Set2FACallback(get2FACode);
        }

        private async Task<string> get2FACode()
        {
            await Task.Yield();
            System.Console.WriteLine("Insert 2FA code:");
            return System.Console.ReadLine();
        }

        protected override async Task onMessage(string mid = null, string author_id = null, string message = null, FB_Message message_object = null, string thread_id = null, ThreadType? thread_type = null, long ts = 0, JToken metadata = null, JToken msg = null)
        {
            System.Console.WriteLine(string.Format("Got new message from {0}: {1}", author_id, message));
            await Task.Yield();
        }

        protected override async Task DeleteCookiesAsync()
        {
            await Task.Yield();
        }

        protected override async Task<Dictionary<string, List<Cookie>>> ReadCookiesFromDiskAsync()
        {
            await Task.Yield();
            return null;
        }

        protected override async Task WriteCookiesToDiskAsync(Dictionary<string, List<Cookie>> cookieJar)
        {
            await Task.Yield();
        }
    }
}
