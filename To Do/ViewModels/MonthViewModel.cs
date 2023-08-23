using Prism.Events;
using To_Do.Services;
using To_Do.Shared;
using To_Do.Views;

namespace To_Do.ViewModels;

internal class MonthViewModel : TaskCollectionViewModel
{
    public MonthViewModel(IToDoApi service, IEventAggregator aggregator) 
        : base(service, TaskType.MONTH, aggregator) {  }

    public override string ViewTitle => MonthView.Title;
}