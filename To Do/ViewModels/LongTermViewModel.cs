using Prism.Events;
using To_Do.Services;
using To_Do.Shared;
using To_Do.Views;

namespace To_Do.ViewModels;

internal class LongTermViewModel : ToDoBaseViewModel
{
    public LongTermViewModel(IToDoApi service, IEventAggregator aggregator) 
        : base(service, TaskType.LONGTERM, aggregator) {  }

    public override string ViewTitle => LongTermView.Title;
}