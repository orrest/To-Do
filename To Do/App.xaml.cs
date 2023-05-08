using AutoMapper;
using Prism.DryIoc;
using Prism.Ioc;
using Refit;
using System.Windows;
using To_Do.Services;

namespace To_Do;

public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        return Container.Resolve<MainView>();
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
        //containerRegistry.RegisterForNavigation<MainView, MainViewModel>();

        /*dialog*/
        //containerRegistry.RegisterDialog<SomeDialog>();
    }
}
