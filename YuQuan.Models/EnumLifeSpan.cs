namespace YuQuan.Models
{
    public enum EnumLifeSpan
    {
        /// <summary>
        /// Intermediate or temporary objects. e.g. Facts in Fact Cache/Profile
        /// </summary>
        Volatile,
        /// <summary>
        /// Instances that are persistently stored in DB as log or history
        /// </summary>
        Persistent,
        /// <summary>
        /// For demo purpose only
        /// </summary>
        Temporary
    }
}