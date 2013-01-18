using System;
using System.Collections.Generic;

namespace YuQuan.Helpers
{
    public class TermTranslationHelper
    {
        private static bool isInitialized = false;

        /// <summary>
        /// This is an English to Chinese dictionary
        /// </summary>
        private static Dictionary<String, String> dictionary = new Dictionary<String, String>();

        public static String Translate(String term)
        {
            if (isInitialized == false)
            {
                PopulateDictionary(ref dictionary);
                isInitialized = true;
            }

            if (dictionary.ContainsKey(term))
                return dictionary[term];
            else
                return term;
        }

        // [TODO] In real application, retrive from database or config xml
        public static void PopulateDictionary(ref Dictionary<String, String> dict)
        {
            #region English2Chinese

            dict.Add("Anesthesia","麻醉");
            dict.Add("Examination","检查");
            dict.Add("Medication","药疗");
            dict.Add("Nurse_CheckList","护理工作");
            dict.Add("Nurse CheckList","护理工作");
            dict.Add("Nursing","护理");
            dict.Add("Diet","膳食");
            dict.Add("Physician_CheckList","诊疗工作");
            dict.Add("Physician CheckList","诊疗工作");
            dict.Add("Surgery","手术");
            dict.Add("Test","检验");
            dict.Add("Treatment","处置");
            dict.Add("Other","其它");

            dict.Add("Longterm", "长期");
            dict.Add("Temporary", "临时");

            #endregion


            #region Chinese2English

            dict.Add("麻醉","Anesthesia");
            dict.Add("检查", "Examination");
            dict.Add("药疗", "Medication");
            dict.Add("护理工作", "Nurse_CheckList");
            dict.Add("护理", "Nursing");
            dict.Add("膳食", "Diet");
            dict.Add("诊疗工作","Physician_CheckList");
            dict.Add("手术", "Surgery");
            dict.Add("检验","Test");
            dict.Add("处置", "Treatment");
            dict.Add("其它", "Other");

            dict.Add("长期", "Longterm");
            dict.Add("临时", "Temporary");

            #endregion
        }
    }
}
