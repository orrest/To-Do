using System.Windows;
using System.Windows.Controls;

namespace To_Do.Controls
{
    public class CheckIcon : Control
    {
        static CheckIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckIcon), new FrameworkPropertyMetadata(typeof(CheckIcon)));
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(CheckIcon), new PropertyMetadata(false));


    }
}
