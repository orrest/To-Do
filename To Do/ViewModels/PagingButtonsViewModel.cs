using Prism.Commands;
using Prism.Mvvm;
using System;

namespace To_Do.ViewModels;

public class PagingButtonsViewModel : BindableBase
{
    private int currentPage;
    public int CurrentPage
    {
        get { return currentPage; }
        set { currentPage = value; RaisePropertyChanged(); }
    }

    private int totalPages;
    public int TotalPages
    {
        get { return totalPages; }
        set { totalPages = value; RaisePropertyChanged(); }
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


    public DelegateCommand PageBackCommand { get; private set; }
    public DelegateCommand PageForwardCommand { get; private set; }
    public DelegateCommand PageRefreshCommand { get; private set; }

    public PagingButtonsViewModel()
    {
        PageRefreshCommand = new DelegateCommand(Refresh);
        PageForwardCommand = new DelegateCommand(Forward);
        PageBackCommand = new DelegateCommand(Backward);
    }

    private void Refresh()
    {
        throw new NotImplementedException();
    }

    private void Forward()
    {
        throw new NotImplementedException();
    }

    private void Backward()
    {
        throw new NotImplementedException();
    }
}
