using System;
using System.ComponentModel;
using System.Linq;

namespace FacebookMessengerCsharp.Helper
{
    public static class EnumHelper
    {
        public static string GetDescription(object obj)
        {
            var field = obj.GetType().GetField(obj.ToString());
            var attributes = field.GetCustomAttributes(false);
            dynamic displayAttribute = null;
            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }
            return displayAttribute?.Description ?? string.Empty;
        }
        public static T GetEnum<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) return default(T);//throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description.Replace(" ", "") == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }
    }
}
