using System.ComponentModel;
namespace YuQuan.Models
{
    /// <summary>
    /// A route of administration in pharmacology and toxicology is the path by which a drug, fluid, poison, or other substance is taken into the body.
    /// </summary>
    public enum EnumAdministrationRoute
    {
        [Description("口服")]
        Oral,

        [Description("静滴")]
        Intravenous,

        [Description("经表皮")]
        Epicutaneous,

        [Description("皮内注射")]
        Intradermal, // skin testing allergy

        [Description("皮下注射")]
        Subcutaneous, // insulin injection

        [Description("经鼻腔")]
        Nasal,

        [Description("经阴道")]
        Intravaginal,

        [Description("雾化吸入")]
        Inhalational, // asthma

        [Description("耳部滴注")]
        EarDrops,

        [Description("眼部滴注")]
        EyeDrops
    }
}
