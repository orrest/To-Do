using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

public class MonthViewModel : ToDoBaseViewModel
{
    public MonthViewModel(IToDoTaskService service) : base("тбхннЯ", service, TaskType.MONTH)
    {

    }
}