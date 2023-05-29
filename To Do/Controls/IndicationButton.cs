using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace To_Do.Controls
{
    public class IndicationButton : Control
    {
        static IndicationButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IndicationButton), new FrameworkPropertyMetadata(typeof(IndicationButton)));
        }

        public PackIconKind IconKind
        {
            get { return (PackIconKind)GetValue(IconKindProperty); }
            set { SetValue(IconKindProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconKind.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register("IconKind", typeof(PackIconKind), typeof(IndicationButton), new PropertyMetadata(PackIconKind.Abacus));

        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(IndicationButton), new PropertyMetadata(18.0));



        public double ButtonSize
        {
            get { return (double)GetValue(ButtonSizeProperty); }
            set { SetValue(ButtonSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonSizeProperty =
            DependencyProperty.Register("ButtonSize", typeof(double), typeof(IndicationButton), new PropertyMetadata(32.0));

        // TODO: ICommand
    }
}
