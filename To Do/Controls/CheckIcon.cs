using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace To_Do.Controls
{
    public class CheckIcon : Control
    {
        static CheckIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckIcon), new FrameworkPropertyMetadata(typeof(CheckIcon)));
        }

        #region IsChecked
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(CheckIcon), new PropertyMetadata(false));
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
                typeof(CheckIcon),
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
                typeof(CheckIcon));

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
