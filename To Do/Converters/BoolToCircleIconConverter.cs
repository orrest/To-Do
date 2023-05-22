using System;
using System.Globalization;
using System.Windows.Data;

namespace To_Do.Converters;

public class BoolToCircleIconConveter : IValueConverter
{
    public readonly string CIRCLE_OUTLINE = "CircleOutline";
    public readonly string CIRCLE = "CheckCircle";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var v = (bool)value;
        return v ? CIRCLE : CIRCLE_OUTLINE;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}