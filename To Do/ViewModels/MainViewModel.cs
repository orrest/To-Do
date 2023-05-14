using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using To_Do.Helpers;
using To_Do.Models;

namespace To_Do.ViewModels;

public class MainViewModel : BindableBase
{
    public DelegateCommand<MenuItem> NavigationCommand { get; private set; }

    public ObservableCollection<MenuItem> MenuItems { get; private set; }
		= new ObservableCollection<MenuItem>();

    private MenuItem? selectedItem;

    public MenuItem? SelectedItem
    {
        get { return selectedItem; }
        set { selectedItem = value; RaisePropertyChanged(); }
    }

    private readonly IRegionManager regionManager;

    public MainViewModel(IRegionManager regionManager)
	{
        NavigationCommand = new DelegateCommand<MenuItem>(Naivgate);
        InitMenuItems();

        this.regionManager = regionManager;
    }

    private void Naivgate(MenuItem menu)
    {
        regionManager.RequestNavigate(Constants.MAIN_REGION, menu.ViewPath);
    }

    private void InitMenuItems()
    {
        MenuItems.Add(new MenuItem
        {
            Icon = "ExclamationThick",
            Color = "#7b8791",
            Title = "重要",
            ViewPath = Constants.URGENT_VIEW
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarTodayOutline",
            Color = "#a9828a",
            Title = "日任务",
            ViewPath = Constants.DAY_VIEW
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarWeekOutline",
            Color = "#78b1ad",
            Title = "周任务",
            ViewPath = Constants.WEEK_VIEW
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarMonthOutline",
            Color = "#84b09d",
            Title = "月任务",
            ViewPath = Constants.MONTH_VIEW
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarCheckOutline",
            Color = "#bac8d4",
            Title = "长期任务",
            ViewPath = Constants.LONGTERM_VIEW
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "EmailArrowLeftOutline",
            Color = "#9b2a46",
            Title = "电子邮件",
            ViewPath = Constants.EMAIL_VIEW
        });
    }
}