using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

public class LongTermViewModel : ToDoBaseViewModel
{
    public LongTermViewModel(IToDoTaskService service) : base("长期任务", service, TaskType.LONGTERM)
    {
    }
}