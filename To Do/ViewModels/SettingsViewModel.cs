using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using To_Do.Helpers;
using To_Do.Views;

namespace To_Do.ViewModels;

public class SettingsViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager regionManager;

    public DelegateCommand SignOutCommand { get; private set; }
    public DelegateCommand NavigateBackCommand { get; private set; }
    public SnackbarMessageQueue MessageQueue { get; set; }

    public SettingsViewModel(IRegionManager regionManager)
    {
        SignOutCommand = new DelegateCommand(SignOut);
        NavigateBackCommand = new DelegateCommand(NavigateBack);
        MessageQueue = new SnackbarMessageQueue();
        this.regionManager = regionManager;
    }

    private void SignOut()
    {
        MessageQueue.Enqueue("Token已删除, 需重新登录");
        SecretHelper.DeleteToken();
    }

    private void NavigateBack()
    {
        regionManager.RequestNavigate(Helpers.Constants.MAIN_CONTENT_REGION,
            nameof(MainView));
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
    }
}
