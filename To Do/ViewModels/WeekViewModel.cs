using Prism.Events;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class WeekViewModel : ToDoBaseViewModel
{
    public WeekViewModel(IToDoApi service, IEventAggregator aggregator) 
        : base("÷‹»ŒŒÒ", service, TaskType.WEEK, aggregator) { }
}