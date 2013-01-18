using System.ComponentModel;
namespace YuQuan.Models
{
    /// <summary>
    /// Status token for Rule Set, Context item, and Problem definition.
    /// For version control.
    /// </summary>
    public enum EnumItemStatus
    {
        [Description("有效")]
        Effective,
        [Description("作废")]
        Expired
    }
}