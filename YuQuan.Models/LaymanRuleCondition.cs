
namespace YuQuan.Models
{
    public class LaymanRuleCondition
    {
        public string Name { get; set; }
        public EnumDataType DataType { get; set; }
        public string Unit { get; set; }
        public EnumComparisonOperator InnerOperator { get; set; }
        public double NumericValue { get; set; }
        public bool BooleanValue { get; set; }
        public string StringValue { get; set; }
        public EnumLogicalOperator OuterOperator { get; set; } //connect 2 conditions
    }
}
