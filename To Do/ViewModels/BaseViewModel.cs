using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Threading.Tasks;

namespace To_Do.ViewModels;

public abstract class BaseViewModel : BindableBase
{
    public abstract string ViewTitle { get; }

    private bool isEmptyList;
    public bool IsEmptyList
    {
        get { return isEmptyList; }
        set { isEmptyList = value; RaisePropertyChanged(); }
    }

    private bool isLoading;
    public bool IsLoading
    {
        get { return isLoading; }
        set { isLoading = value; RaisePropertyChanged(); }
    }
    public PagingButtonsViewModel PagingVm { get; private set; }
    public DelegateCommand PageBackCommand { get; private set; }
    public DelegateCommand PageForwardCommand { get; private set; }
    public DelegateCommand PageRefreshCommand { get; private set; }

    protected IEventAggregator aggregator;

    public BaseViewModel() {  }

    public BaseViewModel(
        IEventAggregator aggregator)
    {
        this.aggregator = aggregator;
        PagingVm = new PagingButtonsViewModel();
        PageRefreshCommand = new DelegateCommand(PagingRefresh);
        PageForwardCommand = new DelegateCommand(PagingForward);
        PageBackCommand = new DelegateCommand(PagingBackward);
    }

    public async void PagingRefresh()
    {
        await FetchItems(PagingButtonsViewModel.FIRST_PAGE);
    }

    public async void PagingForward()
    {
        if (!PagingVm.IsForwardEnable)
        {
            return;
        }

        await FetchItems(PagingVm.NextPage);
    }

    public async void PagingBackward()
    {
        if (!PagingVm.IsBackwardEnable)
        {
            return;
        }

        await FetchItems(PagingVm.PreviousPage);
    }

    public abstract Task FetchItems(int pageIndex);

    public abstract void InitFetch();

    public void OpenLoading()
    {
        IsLoading = true;
    }

    public void CloseLoading(bool hasContent)
    {
        IsLoading = false;
        IsEmptyList = !hasContent;
    }
}
