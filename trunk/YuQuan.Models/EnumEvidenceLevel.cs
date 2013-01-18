using System.ComponentModel;
namespace YuQuan.Models
{
    /// <summary>
    /// Data available from clinical trials or registries about the usefulness/efficacy in different subpopulations, such as gender, age, history of diabetes, history of prior myo-cardial infarction, history of heart failure, and prior aspirin use.
    /// </summary>
    public enum EnumEvidenceLevel
    {
        /// <summary>
        /// Multiple (3-5) population risk strata evaluated. General consistency of direction and magnitude of effect
        /// </summary>
        [Description("Level A")]
        A,
        /// <summary>
        /// Limited (2-3) population risk strata evaluated
        /// </summary>
        [Description("Level B")]
        B,
        /// <summary>
        /// Very limited (1-2) population risk strata evaluated
        /// </summary>
        [Description("Level C")]
        C
    }
}