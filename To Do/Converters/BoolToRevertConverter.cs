using System;
using System.Globalization;
using System.Windows.Data;

namespace To_Do.Converters;

public class BoolToRevertConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool b = (bool)value;
        return !b;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
