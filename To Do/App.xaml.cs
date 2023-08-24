using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Refit;
using System.IO;
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

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        var service = Container.Resolve<IToDoApi>();
        var aggregator = Container.Resolve<IEventAggregator>();

        try
        {
            var user = SecretHelper.LocalLoginDTO();
            var response = await service.LoginAsync(user);

            // response
            if (response.IsSuccessStatusCode)
            {
                aggregator.PublishMessage("ToDo", "自动登录成功");
                await SecretHelper.SaveTokenAsync(response.Content, user.Email, user.Password);
            }
            else
            {
                aggregator.PublishMessage("ToDo", "自动登录失败, 请手动登录");
            }
        }
        catch (FileNotFoundException ex)
        {
            aggregator.PublishMessage("ToDo", "自动登录失败, 请手动登录");
        }
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
