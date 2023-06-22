using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class MonthViewModel : ToDoBaseViewModel
{
    public MonthViewModel(IToDoTaskService service) : base("ÔÂÈÎÎñ", service, TaskType.MONTH)
    {

    }

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}