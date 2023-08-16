using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace To_Do.Converters;

internal class DateVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((string)value).Equals(Helpers.Constants.COUNTDOWN_DATEPICK_DATE)
            ? Visibility.Visible
            : Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
