using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace To_Do.ViewModels;

public class CountdownCreateDialogViewModel : BindableBase, IDialogAware
{
    public string Title => "CountdownCreateDialog";

    public event Action<IDialogResult> RequestClose;

    public DelegateCommand CancelCommand { get; private set; }

    public CountdownCreateDialogViewModel()
    {
        CancelCommand = new DelegateCommand(CancelDialog);
    }

    private void CancelDialog()
    {
        RequestClose?.Invoke(null);
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
