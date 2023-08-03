using Prism.Events;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class MonthViewModel : ToDoBaseViewModel
{
    public MonthViewModel(IToDoApi service, IEventAggregator aggregator) 
        : base("ÔÂÈÎÎñ", service, TaskType.MONTH, aggregator) {  }
}