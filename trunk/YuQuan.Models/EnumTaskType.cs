using System.ComponentModel;
namespace YuQuan.Models
{
    /// <summary>
    /// 医疗任务分类，医疗任务包含若干可选医嘱
    /// 这种分类是对临床路径项目的一种组织，与CPOE/EMR的业务实现相关。
    /// 比如，检验类任务可以直接生成检验申请单
    /// </summary>
    public enum EnumTaskType
    {
        /// <summary>
        /// 可直接生成医嘱
        /// </summary>
        [Description("医嘱")]
        医嘱 = 0,

        /// <summary>
        /// 可直接生成检查申请单
        /// </summary>
        [Description("检查")]
        检查 = 1,

        /// <summary>
        /// 可直接生成检验申请单
        /// </summary>
        [Description("检验")]
        检验 = 2,

        /// <summary>
        /// 可直接生成手术申请单
        /// </summary>
        [Description("手术")]
        手术 = 3,

        /// <summary>
        /// 管理性及指导类诊疗/护理工作
        /// </summary>
        [Description("诊疗工作")]
        诊疗工作 = 4
    }
}