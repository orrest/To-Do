using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading.Tasks;

namespace To_Do.ViewModels;

public abstract class PagingViewModel : BindableBase, INavigationAware
{
    public abstract string ViewTitle { get; }
    public bool IsInitialized { get; set; }

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

    public PagingViewModel() {  }

    public PagingViewModel(
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

    /// <summary>
    /// Fetch data when navigated to.
    /// See OnNavigatedTo.
    /// </summary>
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

    /// <summary>
    /// Fetch data when navigated to.
    /// See InitFetch.
    /// </summary>
    /// <param name="navigationContext"></param>
    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (!IsInitialized)
        {
            IsInitialized = true;
            InitFetch();
        }
    }

    /// <summary>
    /// Determine whether this vm can be navigated to.
    /// Can use the navigation parameters to determine:
    ///     var person = navigationContext.Parameters["person"] as Person;
    /// or the vm's inner state.
    /// 
    /// Default returns true.
    /// </summary>
    /// <param name="navigationContext"></param>
    /// <returns></returns>
    public virtual bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
