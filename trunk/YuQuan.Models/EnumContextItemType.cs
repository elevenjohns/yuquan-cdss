using System.ComponentModel;
namespace YuQuan.Models
{
    public enum EnumContextItemType
    {
        [Description("人口统计学")]
        Demograph,
        [Description("临床所见")]
        Finding,
        [Description("体征")]
        Physiology, // VitalSign
        [Description("检验")]
        LabTest,
        [Description("临床问题")]
        ClinicalProblem
    }
}