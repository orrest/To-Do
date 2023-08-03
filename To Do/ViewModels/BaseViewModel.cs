using Prism.Events;
using Prism.Mvvm;
using To_Do.Events;

namespace To_Do.ViewModels;

public abstract class BaseViewModel : BindableBase
{
    private readonly IEventAggregator aggregator;

    public BaseViewModel(IEventAggregator aggregator)
    {
        this.aggregator = aggregator;
    }

    public void OpenLoading()
    {
        aggregator.GetEvent<LoadingEvent>().Publish(true);
    }

    public void CloseLoading()
    {
        aggregator.GetEvent<LoadingEvent>().Publish(false);
    }
}
