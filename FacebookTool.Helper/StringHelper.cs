using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FacebookTool.Helper
{
    public static class StringHelper
    {
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static string RandomStringWithNumber(int length)
        {
            
                var builder = new StringBuilder();

                const string pool = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                Random random = new Random();
                for (var i = 0; i < length; i++)
                {
                    var c = pool[random.Next(0, pool.Length)];
                    builder.Append(c);
                }
                return builder.ToString();
        }
        

        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: return null;
                case "": return "";
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string CareerOccupation(string input)
        {
            var result = string.Empty;
            try
            {
                result = String.Join("", input.Split(',')).Trim();
                result = Regex.Replace(result, @"\s+", ",");
            }
            catch (Exception e)
            {

            }

            return result;
        }


        public static string JoinWithProperty<T>(T src, string probName, string fieldName)
        {
            string result = null;
            try
            {
                var listPropertyObject = src.GetType().GetProperties();
                foreach (var item in listPropertyObject)
                {
                    if (item.Name == probName)
                    {
                        var listByProbName = item.GetValue(src);
                        var listTest = ((IEnumerable)listByProbName).Cast<object>().ToList();
                        var temp = listTest
                            .Where(x => !string.IsNullOrEmpty(x.GetType().GetProperty(fieldName)?.GetValue(x)?.ToString()))
                            .Select(p => p.GetType().GetProperty(fieldName)?.GetValue(p)?.ToString());
                        result = string.Join(", ", temp);
                        break;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }


        public static string GetCityAndLocation(this string input)
        {
            if (string.IsNullOrEmpty(input))
                //return null;
                return "Nước ngoài";

            input = input.ConvertVN();

            if (input.CheckContainExtension("angiang|an giang"))
                return "An Giang";

            if (input.CheckContainExtension("baria-vungtau|baria|bai ria|vung tau"))
                return "Bà Rịa-Vũng Tàu";

            if (input.CheckContainExtension("bacgiang|bac giang"))
                return "Bắc Giang";

            if (input.CheckContainExtension("backan|baccan|bac kan|bac can"))
                return "Bắc Cạn";


            if (input.CheckContainExtension("baclieu|bac lieu"))
                return "Bạc Liêu";

            if (input.CheckContainExtension("bacninh|bac ninh"))
                return "Bắc Ninh";

            if (input.CheckContainExtension("bentre|ben tre"))
                return "Bến Tre";

            if (input.CheckContainExtension("binhdinh|binh dinh"))
                return "Bình Định";

            if (input.CheckContainExtension("binhduong|binh duong"))
                return "Bình Dương";

            if (input.CheckContainExtension("binhphuoc|binh phuoc"))
                return "Bình Phước";

            if (input.CheckContainExtension("binhthuan|binh thuan"))
                return "Bình Thuận";

            if (input.CheckContainExtension("camau|ca mau"))
                return "Cà Mau";

            if (input.CheckContainExtension("cantho|can tho"))
                return "Cần Thơ";

            if (input.CheckContainExtension("caobang|cao bang"))
                return "Cao bằng";

            if (input.CheckContainExtension("danang|da nang"))
                return "Đà Nẵng";

            if (input.CheckContainExtension("daklak|daclac|dak lak|dac lac"))
                return "Đắc Lắc";

            if (input.CheckContainExtension("dienbien|dien bien"))
                return "Điện Biên";

            if (input.CheckContainExtension("dongnai|dong nai"))
                return "Đồng Nai";

            if (input.CheckContainExtension("dongthap|dong thap"))
                return "Đồng Tháp";

            if (input.CheckContainExtension("gialai|gia lai"))
                return "Gia Lai";

            if (input.CheckContainExtension("hagiang|ha giang"))
                return "Hà Giang";

            if (input.CheckContainExtension("hanam|ha nam"))
                return "Hà Nam";

            if (input.CheckContainExtension("hanoi|ha noi"))
                return "Hà Nội";

            if (input.CheckContainExtension("hatay|hay tay"))
                return "Hà Tây";

            if (input.CheckContainExtension("hatinh|ha tinh"))
                return "Hà Tĩnh";

            if (input.CheckContainExtension("haiduong|hai duong"))
                return "Hải Dương";

            if (input.CheckContainExtension("haiphong|hai phong"))
                return "Hải Phòng";

            if (input.CheckContainExtension("haugiang|hau giang"))
                return "Hậu Giang";

            if (input.CheckContainExtension("hochiminh|ho chi minh|hcm"))
                return "Hồ Chí Minh";

            if (input.CheckContainExtension("hoabinh|hoa binh"))
                return "Hòa Bình";

            if (input.CheckContainExtension("hungyen|hung yen"))
                return "Hưng Yên";

            if (input.CheckContainExtension("khanhhoa|khanh hoa"))
                return "Khánh Hòa";

            if (input.CheckContainExtension("kiengiang|kien giang"))
                return "Kiên Giang";

            if (input.CheckContainExtension("kontum|kon tum"))
                return "Kon Tum";

            if (input.CheckContainExtension("laichau|lai chau"))
                return "Lai Châu";

            if (input.CheckContainExtension("lamdong|lam dong"))
                return "Lâm Đồng";

            if (input.CheckContainExtension("langson|lang son"))
                return "Lạng Sơn";

            if (input.CheckContainExtension("laocai|lao cai"))
                return "Lào Cai";

            if (input.CheckContainExtension("longan|long an"))
                return "Long An";

            if (input.CheckContainExtension("namdinh|nam dinh"))
                return "Nam Định";

            if (input.CheckContainExtension("nghean|nghe an"))
                return "Nghệ An";

            if (input.CheckContainExtension("ninhbinh|ninh binh"))
                return "Ninh Bình";

            if (input.CheckContainExtension("ninhthuan|ninh thuan"))
                return "Ninh Thuận";

            if (input.CheckContainExtension("phutho|phu tho"))
                return "Phú Thọ";

            if (input.CheckContainExtension("phuyen|phu yen"))
                return "Phú Yên";

            if (input.CheckContainExtension("quangbinh|quang binh"))
                return "Quảng Bình";

            if (input.CheckContainExtension("quangnam|quang nam"))
                return "Quảng Nam";

            if (input.CheckContainExtension("quangngai|quang ngai"))
                return "Quảng Ngãi";

            if (input.CheckContainExtension("quangninh|quang ninh"))
                return "Quảng Ninh";

            if (input.CheckContainExtension("quangtri|quang tri"))
                return "Quảng trị";

            if (input.CheckContainExtension("soctrang|soc trang"))
                return "Sóc Trăng";

            if (input.CheckContainExtension("sonla|son la"))
                return "Sơn La";

            if (input.CheckContainExtension("tayninh|tay ninh"))
                return "Tây Ninh";

            if (input.CheckContainExtension("thaibinh|thai binh"))
                return "Thái Bình";

            if (input.CheckContainExtension("thainguyen|thai nguyen"))
                return "Thái Nguyên";

            if (input.CheckContainExtension("thanhhoa|thanh hoa"))
                return "Thanh Hóa";

            if (input.CheckContainExtension("thuathien-hue|thua thien hue|hue"))
                return "Thừa Thiên-Huế";

            if (input.CheckContainExtension("tiengiang|tien giang"))
                return "Tiền Giang";

            if (input.CheckContainExtension("travinh|tra vinh"))
                return "Trà Vinh";

            if (input.CheckContainExtension("tuyenquang|tuyen quang"))
                return "Tuyên Quang";

            if (input.CheckContainExtension("vinhlong|vinh long"))
                return "Vĩnh Long";

            if (input.CheckContainExtension("vinhphuc|vinh phuc"))
                return "Vĩnh Phúc";

            if (input.CheckContainExtension("yenbai|yen bai"))
                return "Yên Bái";

            return "Nước ngoài";

        }
        public static bool CheckContainExtension(this string inputOrigin, string text)
        {
            var arr = text.Split('|');
            foreach (var item in arr)
            {
                if (Regex.IsMatch(inputOrigin, item, RegexOptions.IgnoreCase))
                    return true;
            }
            return false;
        }

        public static string ConvertVN(this string chucodau)
        {
            const string FindText = "áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ";
            const string ReplText = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAADEEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYY";
            int index = -1;
            char[] arrChar = FindText.ToCharArray();
            while ((index = chucodau.IndexOfAny(arrChar)) != -1)
            {
                int index2 = FindText.IndexOf(chucodau[index]);
                chucodau = chucodau.Replace(chucodau[index], ReplText[index2]);
            }
            return chucodau;
        }
    }

}
