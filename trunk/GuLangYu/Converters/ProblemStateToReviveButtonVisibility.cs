using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using YuQuan.Models;
using System.Windows;

namespace GuLangYu.Converters
{
    class ProblemStateToReviveButtonVisibility : IValueConverter

    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EnumProblemState state;
            if (Enum.TryParse<EnumProblemState>(value as string, true, out state) == true)
            {
                switch (state)
                {
                    case EnumProblemState.Dismissed:
                        return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

