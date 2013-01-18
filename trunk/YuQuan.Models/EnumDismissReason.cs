using System.ComponentModel;
namespace YuQuan.Models
{
    public enum EnumDismissReason
    {
        [Description("The provided problem is false positive. Recommendation is incorrect.")]
        Incorrect,
        [Description("This problem has already been treated in the past.")]
        AlreadyTreated,
        [Description("This problem is of no clinical importance.")]
        Trivial
    }
}