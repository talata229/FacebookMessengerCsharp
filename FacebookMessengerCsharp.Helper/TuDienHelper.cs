using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMessengerCsharp.Helper
{
    public static class TuDienHelper
    {
        public static Dictionary<string, string> GenerateVietNameseDictionary()
        {
            var dic = new Dictionary<string, string>();
            string path = "tudientiengviet.txt";
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string line;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var arr = line.Split(':');
                            if (arr.Length == 2)
                            {
                                if (!dic.ContainsKey(arr[0]))
                                {
                                    dic.Add(arr[0], arr[1]);
                                }
                            }
                        }
                    }
                }

                return dic;
            }
        }

        public static Dictionary<string, string> GenerateEnglishDictionary()
        {
            var dic = new Dictionary<string, string>();
            string path = "tudientiengviet.txt";
            using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        string line;
                        int i = 0;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var arr = line.Split(':');
                            if (arr.Length == 2)
                            {
                                if (!dic.ContainsKey(arr[0]))
                                {
                                    dic.Add(arr[0], arr[1]);
                                }
                            }
                        }
                    }
                }

                return dic;
            }
        }

        public static string LastWord(this string text)
        {
            var arr = text.Split(' ');
            if (arr.Length > 1)
            {
                return arr[arr.Length - 1];
            }
            return text;
        }
        public static string FirstWord(this string text)
        {
            var arr = text.Split(' ');
            if (arr.Length > 1)
            {
                return arr[0];
            }
            return text;
        }
    }
}