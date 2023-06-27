using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace To_Do.Controls;


public class AddingTextBox : TextBox
{
    static AddingTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AddingTextBox), new FrameworkPropertyMetadata(typeof(AddingTextBox)));
    }

    #region Return Key Command
    public object CommandParameter
    {
        get { return (object)GetValue(CommandParameterProperty); }
        set { SetValue(CommandParameterProperty, value); }
    }

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(
            "CommandParameter",
            typeof(object),
            typeof(AddingTextBox),
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
            typeof(AddingTextBox));

    /// <summary>
    /// This event is triggered by the operating system, and the Command
    /// is just a bindable property, override the system event to make 
    /// the Command property applied.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
        if (e.Key.Equals(Key.Return))
        {
            ICommand command = Command;
            if (command == null) return;

            if (command.CanExecute(null))
                command.Execute(CommandParameter);
        }

        base.OnKeyDown(e);
    }
    #endregion
}
