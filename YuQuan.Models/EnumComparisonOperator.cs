using System.ComponentModel;
namespace YuQuan.Models
{
    /// <summary>
    /// Put "GreaterThanOrEqualTo" and "LessThanOrEqualTo" before "GreaterThan" and "LessThan". For the sake of string.split().     
    /// </summary>
    public enum EnumComparisonOperator
    {
        [Description(">=")]
        GreaterThanOrEqualTo,
        [Description("<=")]
        LessThanOrEqualTo,
        [Description(">")]        
        GreaterThan,
        [Description("<")]
        LessThan
        // Equal NotEqual
    }
}