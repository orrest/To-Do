using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace To_Do.Views
{
    /// <summary>
    /// LoginDialogView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginDialogView : UserControl
    {
        public LoginDialogView()
        {
            InitializeComponent();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = Window.GetWindow(this);
                window.DragMove();
            }
        }
    }
}
