using System.ComponentModel;
namespace YuQuan.Models
{
    /// <summary>
    /// 医嘱分类
    /// </summary>
    public enum EnumOrderType
    {
        /// <summary>
        /// 管理性及指导类诊疗工作
        /// </summary>
        [Description("诊疗工作")]
        诊疗工作,

        /// <summary>
        /// 管理性及指导类护理工作，区别于护理
        /// </summary>
        [Description("护理工作")]
        护理工作,

        [Description("药疗")]
        药疗,

        [Description("检验")]
        检验,

        [Description("检查")]
        检查,

        [Description("手术")]
        手术,

        [Description("护理")]
        护理,

        [Description("麻醉")]
        麻醉,

        [Description("处置")]
        处置,

        [Description("膳食")]
        膳食,

        [Description("其它")]
        其它
    }
}
