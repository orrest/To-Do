using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
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

    public DelegateCommand<MenuItem> NavigationCommand { get; private set; }
    public DelegateCommand OpenLoginDialogCommand { get; private set; }
    public DelegateCommand OpenSettingsViewCommand { get; private set; }
    public ISnackbarMessageQueue MessageQueue { get; private set; }

    public ObservableCollection<MenuItem> MenuItems { get; private set; }
        = new ObservableCollection<MenuItem>();

    private MenuItem? selectedItem;
    public MenuItem? SelectedItem
    {
        get { return selectedItem; }
        set { selectedItem = value; RaisePropertyChanged(); }
    }

    private bool isUserPopupOpen = false;
    public bool IsUserPopupOpen
    {
        get { return isUserPopupOpen; }
        set { isUserPopupOpen = value; RaisePropertyChanged(); }
    }
    
    private bool isLoading;
    public bool IsLoading
    {
        get { return isLoading; }
        set { isLoading = value; RaisePropertyChanged(); }
    }

    private SyncViewModel syncVm;
    public SyncViewModel SyncVm
    {
        get { return syncVm; }
        set { syncVm = value; RaisePropertyChanged(); }
    }

    private AvatarViewModel avatarVm = new AvatarViewModel();
    public AvatarViewModel AvatarVm
    {
        get { return avatarVm; }
        set { avatarVm = value; RaisePropertyChanged(); }
    }


    /// <summary>
    /// For d:DesignInstance
    /// </summary>
    public MainViewModel() {  }

    public MainViewModel(
        IRegionManager regionManager,
        IDialogService dialogService,
        IEventAggregator aggregator)
    {
        NavigationCommand = new DelegateCommand<MenuItem>(Naivgate);
        OpenLoginDialogCommand = new DelegateCommand(OpenLoginDialog);
        OpenSettingsViewCommand = new DelegateCommand(OpenSettingsView);
        SyncVm = new SyncViewModel(aggregator);
        AvatarVm = new AvatarViewModel(aggregator);

        InitMenuItems();

        this.regionManager = regionManager;
        this.dialogService = dialogService;
        this.aggregator = aggregator;
        this.MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));

        aggregator.GetEvent<MessageEvent>().Subscribe((message) =>
        {
            MessageQueue.Enqueue(message);
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
        IsUserPopupOpen = false;
        regionManager.RequestNavigate(Helpers.Constants.MAIN_CONTENT_REGION,
            nameof(SettingsView));
    }

    private void InitMenuItems()
    {
        MenuItems.Add(new MenuItem
        {
            Icon = StaredView.Icon,
            Color = StaredView.Color,
            Title = StaredView.Title,
            ViewPath = nameof(StaredView)
        });
        MenuItems.Add(new MenuItem
        {
            Icon = WeekView.Icon,
            Color = WeekView.Color,
            Title = WeekView.Title,
            ViewPath = nameof(WeekView)
        });
        MenuItems.Add(new MenuItem
        {
            Icon = MonthView.Icon,
            Color = MonthView.Color,
            Title = MonthView.Title,
            ViewPath = nameof(MonthView)
        });
        MenuItems.Add(new MenuItem
        {
            Icon = LongTermView.Icon,
            Color = LongTermView.Color,
            Title = LongTermView.Title,
            ViewPath = nameof(LongTermView)
        });
        MenuItems.Add(new MenuItem
        {
            Icon = CountdownView.Icon,
            Color = CountdownView.Color,
            Title = CountdownView.Title,
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
        Naivgate(MenuItems[1]);
    }
}
