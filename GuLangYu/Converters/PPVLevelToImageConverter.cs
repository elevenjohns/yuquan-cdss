using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using YuQuan.Models;

namespace GuLangYu.Converters
{
    class PPVLevelToImageConverter : IValueConverter

    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            EnumPPVLevel ppv;
            if (Enum.TryParse<EnumPPVLevel>(value as string,true,out ppv) == true)
            {
                switch (ppv)
                {
                    case EnumPPVLevel.Doubtless:
                        return "/GuLangYu;component/Resources/5stars.png";
                    case EnumPPVLevel.Probable:
                        return "/GuLangYu;component/Resources/4stars.png";
                    case EnumPPVLevel.Possible:
                        return "/GuLangYu;component/Resources/2stars.png";
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
