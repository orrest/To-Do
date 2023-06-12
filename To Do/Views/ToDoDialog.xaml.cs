using Prism.Services.Dialogs;
using System.Windows;

namespace To_Do.Views
{
    /// <summary>
    /// ToDoDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ToDoDialog : Window, IDialogWindow
    {
        public ToDoDialog()
        {
            InitializeComponent();
        }

        public IDialogResult? Result { get; set; }
    }
}
