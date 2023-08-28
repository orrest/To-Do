using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using Prism.Mvvm;

namespace To_Do.ViewModels;

public class PagingButtonsViewModel : BindableBase
{
    private bool isRefreshEnable = true;
    public bool IsRefreshEnable
    {
        get { return isRefreshEnable; }
        set { isRefreshEnable = value; RaisePropertyChanged(); }
    }

    private bool isBackwardEnable;
    public bool IsBackwardEnable
    {
        get { return isBackwardEnable; }
        set { isBackwardEnable = value; RaisePropertyChanged(); }
    }

    private bool isForwardEnable;
    public bool IsForwardEnable
    {
        get { return isForwardEnable; }
        set { isForwardEnable = value; RaisePropertyChanged(); }
    }

    public const int FIRST_PAGE = 0;

    public int CurrentPage { get; set; }
    public int PreviousPage { get; set; }
    public int NextPage { get; set; }
    public int TotalPages { get; set; }

    public PagingButtonsViewModel() {  }

    public void SetPageInfo<T>(IPagedList<T> page)
    {
        CurrentPage = page.PageIndex;
        TotalPages = page.TotalPages;
        PreviousPage = page.HasPreviousPage ? CurrentPage - 1 : CurrentPage;
        NextPage = page.HasNextPage ? CurrentPage + 1 : CurrentPage;
        IsBackwardEnable = page.HasPreviousPage;
        IsForwardEnable = page.HasNextPage;
        IsRefreshEnable = true;
    }
}