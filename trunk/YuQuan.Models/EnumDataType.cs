using System.ComponentModel;
namespace YuQuan.Models
{
    public enum EnumDataType
    {
        [Description("数值型")]
        数值型, // maps to double
        [Description("布尔型")]
        布尔型, // maps to bool
        [Description("字符串型")]
        字符串型 // maps to string
    }
}