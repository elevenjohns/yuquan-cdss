using System.Runtime.Serialization;

namespace YuQuan.Models
{
    [DataContract]
    public class EchoCardiography
    {
        [DataMember]
        public float AOD { get; set; } //升主动脉内径 aortic dimension
        [DataMember]
        public float LAD { get; set; } //左房内径 left atrial dimension
        [DataMember]
        public float LVEF { get; set; } //左心室功能
        [DataMember]
        public float IVS { get; set; } // 室间隔厚度 interventricular septal dimension
        [DataMember]
        public float IVSd { get; set; } //室间隔收缩期厚度 interventricular septal diastolic dimension
        [DataMember]
        public float LVM { get; set; } //left ventricular mass
        [DataMember]
        public float LVMI { get; set; } //left ventricular mass index
        [DataMember]
        public float LVPW { get; set; } //左室后壁厚度
        [DataMember]
        public float LVDs { get; set; } //左心室收缩期内径 left ventricular dimension in systole
        [DataMember]
        public float LVDd { get; set; } //左心室舒张期内径 left ventricular dimension in diastole
        [DataMember]
        public float RVd { get; set; } //right ventricular diastolic dimension
        [DataMember]
        public float FS { get; set; } //左心室缩短分数 fractional shortening
        [DataMember]
        public float EF { get; set; } //射血分数 ejection fraction
        [DataMember]
        public float RWT { get; set; } //relative wall thickness
        [DataMember]
        public float PWd { get; set; } //posterior wall diastolic dimension
    }
}
