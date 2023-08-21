using Prism.DryIoc;
using Prism.Ioc;
using Refit;
using System.Windows;
using To_Do.Helpers;
using To_Do.Secrets;
using To_Do.Services;
using To_Do.ViewModels;
using To_Do.Views;

namespace To_Do;

public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        /*refit*/
        var apiUrl = SecretConstants.RELEASE_URL;
        if (SecretConstants.IS_DEV)
        {
            apiUrl = SecretConstants.DEV_URL;
        }
        var api = RestService.For<IToDoApi>(apiUrl, new RefitSettings()
        {
            AuthorizationHeaderValueGetter = () => SecretHelper.GetTokenAsync(),
            ContentSerializer = new NewtonsoftJsonContentSerializer()
        });
        containerRegistry.RegisterInstance(api);

        /*viewmodels*/
        containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
        containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
        containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        containerRegistry.RegisterForNavigation<CountdownView, CountdownViewModel>();
        containerRegistry.RegisterForNavigation<LongTermView, LongTermViewModel>();
        containerRegistry.RegisterForNavigation<MonthView, MonthViewModel>();
        containerRegistry.RegisterForNavigation<StaredView, StaredViewModel>();
        containerRegistry.RegisterForNavigation<WeekView, WeekViewModel>();
        containerRegistry.RegisterForNavigation<TaskDrawerView, TaskDrawerViewModel>();

        /*dialog*/
        containerRegistry.RegisterDialogWindow<ToDoDialog>();
        containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
        containerRegistry.RegisterDialog<CountdownCreateDialog, CountdownCreateDialogViewModel>();
    }
}
