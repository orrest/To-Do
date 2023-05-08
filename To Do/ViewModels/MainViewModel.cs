using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace To_Do.Services;

public class MainViewModel : BindableBase
{
	private Visibility visibility;

	public Visibility Visibility
    {
		get { return visibility; }
		set { visibility = value; RaisePropertyChanged(); }
	}

	private DelegateCommand openDetailCommand;

	public DelegateCommand OpenDetailCommand
	{
		get { return openDetailCommand; }
		set { openDetailCommand = value; RaisePropertyChanged(); }
	}

	public MainViewModel()
	{
		openDetailCommand = new DelegateCommand(OpenDetail);
	}

    private void OpenDetail()
    {
		if (Visibility == Visibility.Collapsed)
		{
			Visibility = Visibility.Visible;
		}
		else
		{
            Visibility = Visibility.Collapsed;
        }
    }
}