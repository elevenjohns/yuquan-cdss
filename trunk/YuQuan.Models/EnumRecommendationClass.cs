using System.ComponentModel;
namespace YuQuan.Models
{
    /// <summary>
    /// A recommendation with Level of Report B or C does not imply that the recommendation is weak. Many important clinical questions addressed in the guidelines do not lend themselves to clinical trials. Even though randomized trials are not available, there may be a very clear clinical consensus that a particular test or therapy is useful or effective.
    /// </summary>
    public enum EnumRecommendationClass
    {
        /// <summary>
        /// Benefit>>> Risk
        /// Procedure/Treatment SHOULD be performed/administered
        /// </summary>
        [Description("Class I")]
        I,
        /// <summary>
        /// Benefit>> Risk
        /// Additional studies with focused objectives needed.
        /// IT IS REASONABLE to perform procedure/administer treatment
        /// </summary>
        [Description("Class IIA")]
        IIA,
        /// <summary>
        /// Benefit ≥ Risk
        /// Additional studies with broad objectives needed; additional registry data would be helpful
        /// Procedure/Treatment MAY BE CONSIDERED
        /// </summary>
        [Description("Class IIB")]
        IIB,
        /// <summary>
        /// Risk ≥ Benefit
        /// No additional studies needed 
        /// Procedure/Treatment should NOT be performed/administered SINCE IT IS NOT HELPFUL and MAY BE HARMFUL
        /// </summary>
        [Description("Class III")]
        III
    }
}