using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace To_Do.ViewModels
{
    public class LogingDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "";

        public event Action<IDialogResult>? RequestClose;

        public DelegateCommand CloseCommand { get; private set; }

        public LogingDialogViewModel()
        {
            CloseCommand = new DelegateCommand(Close);
        }

        private void Close()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
