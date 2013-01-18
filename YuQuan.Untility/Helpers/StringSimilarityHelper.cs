using System;

namespace YuQuan.Helpers
{
    public class StringSimilarityHelper
    {
        public const float SimilarityThreshold = 0.8f;
        
        public static bool IsSimilar(String s1, String s2)
        {
            if (String.IsNullOrEmpty(s1) == true || 
                String.IsNullOrEmpty(s2) == true)
                return false;

            if (s1.Contains(s2) || s2.Contains(s1))
                return true;

            if (s1.Length >= 3 && s2.Contains(s1.Substring(0, s1.Length - 1)) || 
                s2.Length >= 3 && s1.Contains(s2.Substring(0, s2.Length - 1)))
                return true;

            return false;

            // MatchsMaker matcher = new MatchsMaker(s1, s2);
            // return matcher.Score > SimilarityThreshold;
        }
    }
}
