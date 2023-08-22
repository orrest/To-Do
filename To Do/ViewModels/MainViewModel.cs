using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using To_Do.Events;
using To_Do.Models;
using To_Do.Views;

namespace To_Do.ViewModels;

public class MainViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager regionManager;
    private readonly IDialogService dialogService;
    private readonly IEventAggregator aggregator;
    private MenuItem? selectedItem;
    private bool isUserPopupOpen = false;
    private ISnackbarMessageQueue messageQueue;
    private bool isLoading;

    public DelegateCommand<MenuItem> NavigationCommand { get; private set; }
    public DelegateCommand OpenLoginDialogCommand { get; private set; }
    public DelegateCommand OpenSettingsViewCommand { get; private set; }

    public ObservableCollection<MenuItem> MenuItems { get; private set; }
        = new ObservableCollection<MenuItem>();

    public MenuItem? SelectedItem
    {
        get { return selectedItem; }
        set { selectedItem = value; RaisePropertyChanged(); }
    }

    public bool IsUserPopupOpen
    {
        get { return isUserPopupOpen; }
        set { isUserPopupOpen = value; RaisePropertyChanged(); }
    }

    public ISnackbarMessageQueue MessageQueue
    {
        get { return messageQueue; }
        set { messageQueue = value; }
    }

    public bool IsLoading
    {
        get { return isLoading; }
        set { isLoading = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// For d:DesignInstance
    /// </summary>
    public MainViewModel()
    {
            
    }

    public MainViewModel(
        IRegionManager regionManager,
        IDialogService dialogService,
        IEventAggregator aggregator)
    {
        NavigationCommand = new DelegateCommand<MenuItem>(Naivgate);
        OpenLoginDialogCommand = new DelegateCommand(OpenLoginDialog);
        OpenSettingsViewCommand = new DelegateCommand(OpenSettingsView);

        InitMenuItems();

        this.regionManager = regionManager;
        this.dialogService = dialogService;
        this.aggregator = aggregator;
        messageQueue = new SnackbarMessageQueue();

        aggregator.GetEvent<MessageEvent>().Subscribe((message) =>
        {
            messageQueue.Enqueue(message);
        });
    }

    private void Naivgate(MenuItem menu)
    {
        regionManager.RequestNavigate(Helpers.Constants.SUB_CONTENT_REGION, menu.ViewPath);
    }

    private void OpenLoginDialog()
    {
        IsUserPopupOpen = false;
        dialogService.ShowDialog("LoginView");
    }

    private void OpenSettingsView()
    {
        regionManager.RequestNavigate(Helpers.Constants.MAIN_CONTENT_REGION,
            nameof(SettingsView));
    }

    private void InitMenuItems()
    {
        MenuItems.Add(new MenuItem
        {
            Icon = "ExclamationThick",
            Color = "#7b8791",
            Title = "重要",
            ViewPath = nameof(StaredView)
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarWeekOutline",
            Color = "#78b1ad",
            Title = "周任务",
            ViewPath = nameof(WeekView)
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarMonthOutline",
            Color = "#84b09d",
            Title = "月任务",
            ViewPath = nameof(MonthView)
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarCheckOutline",
            Color = "#bac8d4",
            Title = "长期任务",
            ViewPath = nameof(LongTermView)
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "TimerStarOutline",
            Color = "#9b2a46",
            Title = "倒计时",
            ViewPath = nameof(CountdownView)
        });
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {

    }
}
