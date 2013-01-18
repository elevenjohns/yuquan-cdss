using System.ComponentModel;
namespace YuQuan.Models
{
    public enum EnumPPVLevel
    {
        [Description("高(100%)")]
        Doubtless,
        [Description("中(>=80%)")]
        Probable,
        [Description("低(>=40%)")]
        Possible
    }
}