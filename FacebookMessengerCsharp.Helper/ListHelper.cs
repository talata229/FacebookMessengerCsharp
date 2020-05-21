using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Helper
{
    public class ListHelper
    {
        public static List<string> ListUser = new List<string>
            {
                "100042693053214", //kwangtrang229
                "100005048402622",//tranquang229
                "100004086313987"
            };

        public static List<string> ListMessage = new List<string>
            {
                "Chúc 1 ngày tốt lành",
                "Đang làm gì vậy",
                "Chào buổi tối"
            };

        public static string GetRandomItemInList(List<string> list)
        {
            Random rd = new Random();
            int index = rd.Next(list.Count);
            return list[index];
        }
    }
}
