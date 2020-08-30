using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookTool.Helper
{
    public class ListHelper
    {
        public static string GetRandomItemInList(List<string> list)
        {
            Random rd = new Random();
            int index = rd.Next(list.Count);
            return list[index];
        }
    }
}
