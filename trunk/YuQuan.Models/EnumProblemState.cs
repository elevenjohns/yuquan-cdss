namespace YuQuan.Models
{
    public enum EnumProblemState
    {
        New,
        /// <summary>
        /// According to reasonable result, the problem no longer exist. In this case, prompt user to solve it.
        /// </summary>
        ResolvedSuspected,
        Resolved,        
        Snoozed,
        Dismissed        
        // Reviewed,
        // Alerted,
    }
}