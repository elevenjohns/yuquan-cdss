using System.Runtime.Serialization;

namespace YuQuan.Models
{
    [DataContract]
    public class CoronaryAngiography
    {
        [DataMember]
        CoronaryArtery LAD; // Left Anterior Descending
        [DataMember]
        CoronaryArtery DIAG; // Diagonal
        [DataMember]
        CoronaryArtery LCX; // Left Circumflex
        [DataMember]
        CoronaryArtery OM; // Obtuse Marginal Branch
        [DataMember]
        CoronaryArtery RCA; // Right Coronary Artery
        [DataMember]
        CoronaryArtery LM; // Left Main
        [DataMember]
        CoronaryArtery RenalArtery; // Renal Artery is not coronary artery; only use data structure.
    }
}
