using System;
using System.Globalization;
using System.Windows.Data;

namespace To_Do.Converters;

public class BoolToStarIconConverter : IValueConverter
{
    public readonly string STAR_OUTLINE = "StarOutline";
    public readonly string STAR = "Star";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var v = (bool)value;
        return v ? STAR : STAR_OUTLINE;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}