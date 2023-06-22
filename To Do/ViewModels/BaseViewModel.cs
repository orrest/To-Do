using Prism.Mvvm;

namespace To_Do.ViewModels;

internal abstract class BaseViewModel : BindableBase
{
    public bool IsLoading { get; set; }

    public BaseViewModel()
    {
        InitializeWrapper();
    }

    private void InitializeWrapper()
    {
        IsLoading = true;
        Initialize();
        IsLoading = false;
    }

    /// <summary>
    /// This method will be wrap by IsLoading property.
    /// </summary>
    public abstract void Initialize();

}
