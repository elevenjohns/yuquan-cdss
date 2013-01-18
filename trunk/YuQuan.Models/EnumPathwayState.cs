using System.ComponentModel;
namespace YuQuan.Models
{
    public enum EnumPathwayState
    {
        [Description("执行中")]
        InProgress,

        [Description("已完成")]
        Done,

        [Description("已退出")]
        Cancelled
    }
}