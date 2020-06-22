using Facebook.DAL.Requests.Simsimi;
using Facebook.DAL.Responses.Simsimi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Helper
{
    public class SimsimiHelper
    {
        public static List<string> listAPIKey = new List<string>
        {
            //"FP4-_bvambFCy1IJy4PXEWm~iceuVjBU-.VcfkCI", //muathudep105
            //"Ol_zRLJ2Cpiva0QB30UkvZZmaAcpHNyKggxGnRQV", //muathudep106
            //"mQg6ZZqqwpfMY7sZ2XZRqqI-MIMD-eihD_98W-6r" //muathudep107
            //"e0gWyi6igjE2E7zHX5cxHGKX6arFPv3iZqilAPVf",//quangquangtranvan229@gmail.com
            //"fIq8Ux5k2_DxUJZj3pYwB~1lFg-3qqQG_nHyCy67"//quangtranvan229@gmail.com
            "hFqwrc6EGlyCU1tecF-rDp1-Q.a2L9tV-zjEfab2"//muathudep06
        };

        public static string RandomAPIKey(List<string> list)
        {
            Random rd = new Random();
            return list[rd.Next(list.Count)];
        }

        public static async Task<string> SendSimsimi(string question)
        {
            int countTried = 0;
            try
            {
                do
                {
                    var http = new HttpClient();
                    http.DefaultRequestHeaders.Clear();
                    http.DefaultRequestHeaders.Add("x-api-key", RandomAPIKey(listAPIKey));
                    string url = "https://wsapi.simsimi.com/190410/talk";
                    SimsimiRequest request = new SimsimiRequest
                    {
                        Utext = question,
                        Lang = "vn"
                    };
                    string json = JsonConvert.SerializeObject(request);
                    HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await http.PostAsync(url, httpContent);
                    var result = await response.Content.ReadAsStringAsync();
                    SimsimiResponse simsimi = JsonConvert.DeserializeObject<SimsimiResponse>(result);
                    if (simsimi.Status == 200)
                    {
                        return simsimi.Atext;
                    }
                    else
                    {
                        countTried++;
                    }
                } while (countTried > 3);

            }
            catch (Exception e)
            {
            }
            return "";
        }
    }
}
