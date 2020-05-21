using FacebookMessengerCsharp.Client.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Console
{
    class QuangClient : MessengerClient
    {
        protected override async Task DeleteCookiesAsync()
        {
            await Task.Yield();
        }

        protected override Task<Dictionary<string, List<Cookie>>> ReadCookiesFromDiskAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task WriteCookiesToDiskAsync(Dictionary<string, List<Cookie>> cookieJar)
        {
            throw new NotImplementedException();
        }
    }
}
