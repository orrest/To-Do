using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace To_Do.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool b = (bool)value;

        return b ? "Transparent" : "White";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}