using Prism.Events;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class LongTermViewModel : ToDoBaseViewModel
{
    public LongTermViewModel(IToDoApi service, IEventAggregator aggregator) 
        : base("长期任务", service, TaskType.LONGTERM, aggregator) {  }
}