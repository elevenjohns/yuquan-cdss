using System;
using System.ComponentModel;
using System.Reflection;

namespace YuQuan.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static TEnum GetEnumFromDescription<TEnum>(string str)
        {
            var values = Enum.GetValues(typeof(TEnum));
            foreach (TEnum value in values)
            {
                if (GetEnumDescription<TEnum>(value) == str)
                    return value;
            }
            return default(TEnum);
        }
    }
}