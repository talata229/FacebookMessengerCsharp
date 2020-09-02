using System;
using System.Collections.Generic;

namespace FacebookMessengerCsharp.Helper
{
    public class ListHelper<T>
    {
        public static List<string> ListUser = new List<string>
            {
                "100042693053214", //kwangtrang229
                "100005048402622",//tranquang229
            };

        public static T GetRandomItemInListObject(List<T> list)
        {
            Random rd = new Random();
            int index = rd.Next(list.Count);
            return list[index];
        }
      
    }
}
