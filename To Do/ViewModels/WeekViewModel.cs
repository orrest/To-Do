using Prism.Events;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class WeekViewModel : ToDoBaseViewModel
{
    public WeekViewModel()
    {
        ViewTitle = "周任务";
    }

    public WeekViewModel(IToDoApi service, IEventAggregator aggregator) 
        : base("周任务", service, TaskType.WEEK, aggregator) { }
}