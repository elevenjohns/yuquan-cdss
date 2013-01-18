using System;

namespace YuQuan.Models
{
    public partial class MedicalOrderDefinition
    {
        public EnumOrderType OrderTypeInEnum
        {
            get { return (EnumOrderType)Enum.Parse(typeof(EnumOrderType), OrderType); }
            set { OrderType = value.ToString(); }
        }

        public EnumTemporalType TemporalTypeInEnum
        {
            get { return (EnumTemporalType)Enum.Parse(typeof(EnumTemporalType), TemporalType); }
            set { TemporalType = value.ToString(); }
        }

        public EnumAdministrationRoute AdministrationRouteInEnum
        {
            get { return (EnumAdministrationRoute)Enum.Parse(typeof(EnumAdministrationRoute), AdministrationRoute); }
            set { AdministrationRoute = value.ToString(); }
        }

        public static EnumTaskType MapToTaskType(EnumOrderType orderType) 
        {
            switch (orderType)
            {
                case EnumOrderType.处置:
                case EnumOrderType.护理:
                case EnumOrderType.麻醉:
                case EnumOrderType.膳食:
                case EnumOrderType.药疗:
                    return EnumTaskType.医嘱;

                case EnumOrderType.检查:
                    return EnumTaskType.检查;

                case EnumOrderType.检验:
                    return EnumTaskType.检验;

                case EnumOrderType.诊疗工作:
                case EnumOrderType.护理工作:
                    return EnumTaskType.诊疗工作;

                case EnumOrderType.手术:
                    return EnumTaskType.手术;
            }

            return EnumTaskType.医嘱;
        }

        public EnumTaskType MapToTaskType()
        {
            return MapToTaskType(this.OrderTypeInEnum);
        }
    }
}
