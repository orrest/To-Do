using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace To_Do.ViewModels
{
    class LoginDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "";

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand CloseCommand { get; private set; }

        public LoginDialogViewModel()
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
