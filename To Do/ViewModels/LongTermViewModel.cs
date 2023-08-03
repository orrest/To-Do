using Prism.Events;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class LongTermViewModel : ToDoBaseViewModel
{
    public LongTermViewModel(IToDoApi service, IEventAggregator aggregator) 
        : base("��������", service, TaskType.LONGTERM, aggregator) {  }
}