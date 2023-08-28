using Prism.Events;
using Prism.Mvvm;
using To_Do.Helpers;

namespace To_Do.ViewModels;

public class AvatarViewModel : BindableBase
{
	private string color = string.Empty;
	public string Color
	{
		get { return color; }
		set { color = value; RaisePropertyChanged(); }
	}

	private string kind = "AplhaA";
    public string Kind
	{
		get { return kind; }
		set { kind = value; RaisePropertyChanged(); }
	}

	private string email = string.Empty;
	public string Email
	{
		get { return email; }
		set { email = value; RaisePropertyChanged(); }
	}

	private readonly IEventAggregator aggregator = null;

    public AvatarViewModel() {  }

    public AvatarViewModel(IEventAggregator aggregator)
    {
        this.aggregator = aggregator;
		aggregator.SubscribeAvatarInfo(avatar =>
		{
			Color = avatar.Color;
			Kind = avatar.Kind;
			Email = avatar.Email;
		});
    }
}
