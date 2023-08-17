using Prism.Events;
using Prism.Mvvm;

namespace To_Do.ViewModels;

public abstract class BaseViewModel : BindableBase
{
    /// <summary>
    /// 当前视图的标题
    /// </summary>
    protected string viewTitle = "view";
    public string ViewTitle
    {
        get { return viewTitle; }
        set { viewTitle = value; RaisePropertyChanged(); }
    }

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

    protected IEventAggregator aggregator;

    public BaseViewModel()
    {
        
    }

    public BaseViewModel(IEventAggregator aggregator)
    {
        this.aggregator = aggregator;
    }

    public virtual void LoadingItems()
    {

    }

    public void OpenLoading()
    {
        IsLoading = true;
    }

    public void CloseLoading()
    {
        IsLoading = false;
    }
}
