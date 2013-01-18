using System;

namespace YuQuan.Models
{
    public partial class EBM
    {
        public EnumEvidenceLevel EvidenceLevelInEnum
        {
            get { return (EnumEvidenceLevel)Enum.Parse(typeof(EnumEvidenceLevel), EvidenceLevel); }
            set { EvidenceLevel = value.ToString(); }
        }

        public EnumRecommendationClass RecommendationClassInEnum
        {
            get { return (EnumRecommendationClass)Enum.Parse(typeof(EnumRecommendationClass), RecommendationClass); }
            set { RecommendationClass = value.ToString(); }
        }
    }
}
