using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

public class WeekViewModel : ToDoBaseViewModel
{
    public WeekViewModel(IToDoTaskService service) 
        : base("÷‹»ŒŒÒ", service, TaskType.WEEK)
    {
    }
}