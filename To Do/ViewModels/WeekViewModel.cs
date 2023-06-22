using System.Collections.ObjectModel;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class WeekViewModel : ToDoBaseViewModel
{
    public WeekViewModel(IToDoTaskService service) 
        : base("÷‹»ŒŒÒ", service, TaskType.WEEK)
    {

    }

    public override async void Initialize()
    {
    }
}