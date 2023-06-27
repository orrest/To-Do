using System.Windows;
using System.Windows.Controls;
namespace To_Do.Controls;


public class AddingTextBox : TextBox
{
    static AddingTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AddingTextBox), new FrameworkPropertyMetadata(typeof(AddingTextBox)));

        
    }
}
