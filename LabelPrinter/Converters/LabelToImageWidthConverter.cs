using System;
using System.Globalization;
using System.Windows.Data;

namespace LabelPrinter.Converters
{
    class LabelToImageWidthConverter : IValueConverter
    {
        #region Implemented  method(s)

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int length = (int)value;
            return AppConstant.ImageMultiplyWith * length;
        }

        #endregion

        #region Unimplemented method(s)

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        } 

        #endregion
    }
}
