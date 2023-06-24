using Prism.Mvvm;

namespace To_Do.ViewModels;

internal abstract class BaseViewModel : BindableBase
{
    public bool IsLoading { get; set; }
}
