using Prism.Events;
using Prism.Mvvm;
using To_Do.Helpers;

namespace To_Do.ViewModels;

public class SyncViewModel : BindableBase
{
	private string color = string.Empty;
	public string Color
	{
		get { return color; }
		set { color = value; RaisePropertyChanged(); }
	}

	private string info = string.Empty;
    public string Info
	{
		get { return info; }
		set { info = value; RaisePropertyChanged(); }
	}

    private readonly IEventAggregator aggregator = null;

    public SyncViewModel() {  }

    public SyncViewModel(IEventAggregator aggregator)
    {
		this.aggregator = aggregator;

		Color = "Orange";
		Info = "登录中...";

		aggregator.SubscribeSyncInfo(syncInfo =>
		{
			Info = syncInfo.Info;
			Color = syncInfo.Color;
		});
    }
}
