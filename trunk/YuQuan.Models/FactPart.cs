namespace YuQuan.Models
{
    public partial class Fact
    {
        /// <summary>
        /// Shallow copy an object
        /// </summary>
        /// <param name="fact"></param>
        /// <returns></returns>
        /// <remarks> Why not use object.MemberwiseClone() directly?
        /// Because it will cause "The entity wrapper stored in the proxy does not reference the same proxy." error.
        /// When you MemberwiseClone an EF-loaded entity, you're cloning the proxy class as well. One of the things a proxy class carries around is a reference to the wrapper fo the given entity. Because a shallow copy only copies a reference to the wrapper, you suddenly have two entities that have the same wrapper instance.At this point, EF thinks you've created or borrowed a different proxy class for your entity which it assumes is for purposes of mischief and blocks you.</remarks>
        public static Fact Clone(Fact fact)
        {
            var newFact = new Fact();
            newFact.Assign(fact);
            return newFact;
        }

        /// <summary>
        /// Assign values from an existing object
        /// </summary>
        /// <param name="fact"></param>
        public void Assign(Fact fact)
        {
            ContextItemDefinition = fact.ContextItemDefinition;
            Report = fact.Report;
            IsAbnormal = fact.IsAbnormal;
            Confidence = fact.Confidence;
            BooleanValue = fact.BooleanValue;
            NumericValue = fact.NumericValue;
            StringValue = fact.StringValue;
        }

        public object Value()
        {
            if (this.ContextItemDefinition == null)
                return null;

            if (this.ContextItemDefinition.DataType == EnumDataType.数值型.ToString())
                return NumericValue;
            if (this.ContextItemDefinition.DataType == EnumDataType.布尔型.ToString())
                return BooleanValue;
            if (this.ContextItemDefinition.DataType == EnumDataType.字符串型.ToString())
                return StringValue;
            return null;
        }

        public string ValueString()
        {
            if (this.ContextItemDefinition == null)
                return "";

            if (this.ContextItemDefinition.DataType == EnumDataType.数值型.ToString())
                return NumericValue + this.ContextItemDefinition.Unit;
            if (this.ContextItemDefinition.DataType == EnumDataType.布尔型.ToString())
                return BooleanValue.ToString();
            if (this.ContextItemDefinition.DataType == EnumDataType.字符串型.ToString())
                return StringValue;
            return "";
        }
    }
}
