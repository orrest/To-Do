using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class LongTermViewModel : ToDoBaseViewModel
{
    public LongTermViewModel(IToDoTaskService service) : base("长期任务", service, TaskType.LONGTERM)
    {
    }

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}