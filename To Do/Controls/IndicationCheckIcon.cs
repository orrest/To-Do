using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace To_Do.Controls
{
    public class IndicationCheckIcon : Control
    {
        static IndicationCheckIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IndicationCheckIcon), new FrameworkPropertyMetadata(typeof(IndicationCheckIcon)));
        }

        #region IsChecked
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(IndicationCheckIcon), new PropertyMetadata(false));
        #endregion

        #region Click Command
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter", 
                typeof(object),
                typeof(IndicationCheckIcon),
                new FrameworkPropertyMetadata((object)null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command", 
                typeof(ICommand),
                typeof(IndicationCheckIcon));

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
                command.Execute(CommandParameter);

            base.OnMouseLeftButtonUp(e);
        }
        #endregion
    }
}
