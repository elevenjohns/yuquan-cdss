using System;

namespace YuQuan.Models
{
    public partial class ContextItemDefinition
    {
        public EnumDataType DataTypeInEnum
        {
            get { return (EnumDataType)Enum.Parse(typeof(EnumDataType), DataType); }
            set { DataType = value.ToString(); }
        }

        public bool Expired
        {
            get
            {
                // According to version control table.
                return false;
            }
        }
    }
}
