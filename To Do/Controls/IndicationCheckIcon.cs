using System.Windows;
using System.Windows.Controls;

namespace To_Do.Controls
{
    public class IndicationCheckIcon : Control
    {
        static IndicationCheckIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IndicationCheckIcon), new FrameworkPropertyMetadata(typeof(IndicationCheckIcon)));
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(IndicationCheckIcon), new PropertyMetadata(false));


    }
}
