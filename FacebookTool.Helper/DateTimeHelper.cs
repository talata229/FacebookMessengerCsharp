using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookTool.Helper
{
    public class DateTimeHelper
    {
        public static DateTime? ConvertStringFromApiFacebookToDateTime(string inputDateTimeStr)
        {
            try
            {
                if (string.IsNullOrEmpty(inputDateTimeStr))
                {
                    return null;
                }
                return DateTime.ParseExact(inputDateTimeStr, @"yyyy-MM-dd\THH:mm:ssK", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
              
            }
            return null;
        }
    }
}
