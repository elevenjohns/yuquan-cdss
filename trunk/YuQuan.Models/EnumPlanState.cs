namespace YuQuan.Models
{
    public enum EnumPlanState
    {
        /// <summary>
        /// When an order set/ pathway is applied
        /// </summary>
        Applied,

        /// <summary>
        /// When a pathway is finished (by going through all phases)
        /// </summary>
        Finished,

        /// <summary>
        /// When user exits a pathway in the middle way.
        /// </summary>
        Cancelled
    }
}