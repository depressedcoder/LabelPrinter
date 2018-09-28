using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace LabelPrinter.Converters
{
    public class RadioBoolToIntConverter : IValueConverter
    {
        #region Implemented  method(s)

        //SETUPWINDOW radio buttons converter to int value
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int)value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        #endregion

        #region Unimplemented method(s)

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }

        #endregion
    }
}
