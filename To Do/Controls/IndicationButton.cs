using MaterialDesignThemes.Wpf;
using System.Reflection;
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

        #region Icon kind
        public PackIconKind IconKind
        {
            get { return (PackIconKind)GetValue(IconKindProperty); }
            set { SetValue(IconKindProperty, value); }
        }

        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register("IconKind", typeof(PackIconKind), typeof(IndicationButton), new PropertyMetadata(PackIconKind.Abacus));
        #endregion

        #region Icon size
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(IndicationButton), new PropertyMetadata(18.0));
        #endregion

        #region Button size
        public double ButtonSize
        {
            get { return (double)GetValue(ButtonSizeProperty); }
            set { SetValue(ButtonSizeProperty, value); }
        }

        public static readonly DependencyProperty ButtonSizeProperty =
            DependencyProperty.Register("ButtonSize", typeof(double), typeof(IndicationButton), new PropertyMetadata(32.0));
        #endregion

        #region Click Command
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(IndicationButton));

        /// <summary>
        /// This event is triggered by the operating system, and the Command
        /// is just a bindable property, override the system event to make 
        /// the Command property applied.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            ICommand command = Command;
            if (command == null) return;

            if (command.CanExecute(null))
                command.Execute(null);

            base.OnMouseLeftButtonUp(e);
        }
        #endregion
    }
}
