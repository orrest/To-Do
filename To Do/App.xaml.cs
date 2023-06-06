using AutoMapper;
using Prism.DryIoc;
using Prism.Ioc;
using Refit;
using System.Windows;
using To_Do.Services;
using To_Do.ViewModels;
using To_Do.Views;

namespace To_Do;

public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainView>();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        /*refit*/
        var api = RestService.For<IToDoApi>("http://localhost:5000");
        containerRegistry.RegisterInstance(api);

        /*automapper*/
        var config = new MapperConfiguration(cfg =>
        {
            //cfg.CreateMap<ToDoModel, ToDoDto>().ReverseMap();
        });
        containerRegistry.RegisterInstance(config.CreateMapper());

        /*viewmodels*/
        containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
        containerRegistry.RegisterForNavigation<EmailView, EmailViewModel>();
        containerRegistry.RegisterForNavigation<LongTermView, LongTermViewModel>();
        containerRegistry.RegisterForNavigation<MonthView, MonthViewModel>();
        containerRegistry.RegisterForNavigation<UrgentView, UrgentViewModel>();
        containerRegistry.RegisterForNavigation<WeekView, WeekViewModel>();

        /*dialog*/
        containerRegistry.RegisterDialogWindow<Views.DialogWindow>();
        containerRegistry.RegisterDialog<LoginDialogView, LoginDialogViewModel>();
    }
}
