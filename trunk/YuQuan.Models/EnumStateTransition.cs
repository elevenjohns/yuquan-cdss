namespace YuQuan.Models
{
    public enum EnumStateTransition
    {
        // For problem instance
        Create,
        Solve,
        Dismiss,
        Revive,

        // For plan instance
        Apply,
        Exit,
        Finish,
        ReApply
    }
}