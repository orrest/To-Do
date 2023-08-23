using Prism.Events;
using To_Do.Services;
using To_Do.Shared;
using To_Do.Views;

namespace To_Do.ViewModels;

internal class WeekViewModel : TaskCollectionViewModel
{
    public WeekViewModel(IToDoApi service, IEventAggregator aggregator) 
        : base(service, TaskType.WEEK, aggregator) { }

    public override string ViewTitle => WeekView.Title;
}