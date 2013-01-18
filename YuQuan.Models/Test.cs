using System.Runtime.Serialization;

namespace YuQuan.Models
{
    /// <summary>
    /// 化验结果
    /// </summary>
    [DataContract]
    public class Test
    {
        #region 心肌损伤/心肌梗死
        [DataMember]
        public float AST { get; set; }
        [DataMember]
        public float CK { get; set; }
        [DataMember]
        public float CK_MB { get; set; }
        [DataMember]
        public float LDH { get; set; } // Lactate Dehydrogenase
        [DataMember]
        public float LD1 { get; set; }
        [DataMember]
        public float TNT { get; set; }
        #endregion

        #region 血脂
        [DataMember]
        public float TC { get; set; }
        [DataMember]
        public float TG { get; set; }
        [DataMember]
        public float LDL { get; set; }
        [DataMember]
        public float HDL { get; set; }
        /// <summary>
        /// Apolipoprotein A-1
        /// </summary>
        [DataMember]
        public float APOA1 { get; set; }
        /// <summary>
        /// Apolipoprotein B
        /// </summary>
        [DataMember]
        public float APOB { get; set; }
        /// <summary>
        /// LP(a)，脂蛋白
        /// </summary>
        [DataMember]
        public float a { get; set; }
        #endregion

        #region 肾功
        [DataMember]
        public float CRE { get; set; }
        [DataMember]
        public float BUN { get; set; }
        /// <summary>
        /// Uric acid
        /// </summary>
        [DataMember]
        public float UA { get; set; }
        /// <summary>
        /// microglobulin
        /// </summary>
        [DataMember]
        public float Mg { get; set; }
        /// <summary>
        /// Cystatin-C 胱抑素C
        /// </summary>
        [DataMember]
        public float CYC { get; set; }
        #endregion

        #region 血糖
        /// <summary>
        /// fasting blood glucose 空腹血糖
        /// </summary>
        [DataMember]
        public float FBG { get; set; } // 
        /// <summary>
        /// Postprandial blood sugar 餐后血糖
        /// </summary>
        [DataMember]
        public float PPBS { get; set; }
        #endregion

        #region 血常规
        /// <summary>
        /// Hemoglobin 血红蛋白
        /// </summary>
        [DataMember]
        public float HGB { get; set; }
        /// <summary>
        /// 白细胞
        /// </summary>
        [DataMember]
        public float WBC { get; set; }
        /// <summary>
        /// 中性粒细胞
        /// </summary>
        [DataMember]
        public float Neutrophil { get; set; }
        #endregion

        #region 心血管疾病
        /// <summary>
        /// Homocysteine 同型半胱氨酸
        /// </summary>
        [DataMember]
        public float HCY { get; set; }
        /// <summary>
        /// 脑钠肽
        /// </summary>
        [DataMember]
        public float BNP { get; set; }
        /// <summary>
        /// D-dimer
        /// </summary>
        [DataMember]
        public float DD { get; set; }
        /// <summary>
        /// C-reactive protein 
        /// </summary>
        [DataMember]
        public float CRP { get; set; }
        /// <summary>
        /// Interleukin-6
        /// </summary>
        [DataMember]
        public float IL6 { get; set; }
        #endregion

    }
}
