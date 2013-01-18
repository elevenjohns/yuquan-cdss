using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using YuQuan.Models;

namespace GuLangYu.Converters
{
    class ProblemStateToImageConverter : IValueConverter

    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EnumProblemState state;
            if (Enum.TryParse<EnumProblemState>(value as string, true, out state) == true)
            {
                switch (state)
                {
                    //case EnumProblemState.New:
                    //    return "/GuLangYu;component/Resources/blank.png";
                    case EnumProblemState.Resolved:
                        return "/GuLangYu;component/Resources/check.png";
                    case EnumProblemState.Dismissed:
                        return "/GuLangYu;component/Resources/cross.png";
                    case EnumProblemState.ResolvedSuspected:
                        return "/GuLangYu;component/Resources/question.png";
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
