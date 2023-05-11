using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using To_Do.Models;

namespace To_Do.Services;

public class MainViewModel : BindableBase
{
	private Visibility visibility;

	public Visibility Visibility
    {
		get { return visibility; }
		set { visibility = value; RaisePropertyChanged(); }
	}

    public DelegateCommand OpenDetailCommand { get; private set; }

    public DelegateCommand NavigationCommand { get; private set; }

    public ObservableCollection<MenuItem> MenuItems { get; private set; }
		= new ObservableCollection<MenuItem>();

	public MainViewModel()
	{
		OpenDetailCommand = new DelegateCommand(OpenDetail);
        NavigationCommand = new DelegateCommand(Naivgate);
        InitMenuItems();
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

    private void Naivgate()
    {
        throw new NotImplementedException();
    }

    private void InitMenuItems()
    {
        MenuItems.Add(new MenuItem
        {
            Icon = "ExclamationThick",
            Color = "#7b8791",
            Title = "重要",
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarTodayOutline",
            Color = "#a9828a",
            Title = "日任务",
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarWeekOutline",
            Color = "#78b1ad",
            Title = "周任务",
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarMonthOutline",
            Color = "#84b09d",
            Title = "月任务",
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "CalendarCheckOutline",
            Color = "#bac8d4",
            Title = "长期任务",
        });
        MenuItems.Add(new MenuItem
        {
            Icon = "EmailArrowLeftOutline",
            Color = "#9b2a46",
            Title = "电子邮件",
        });
    }

}