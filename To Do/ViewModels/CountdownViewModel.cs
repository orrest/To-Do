using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;

namespace To_Do.ViewModels;

public class CountdownViewModel : BaseViewModel
{
    private readonly IDialogService dialog;

    /// <summary>
    /// 已完成的倒计时项的集合
    /// </summary>
    public ObservableCollection<CountdownItemViewModel> FinishedCountdowns { get; private set; }
        = new ObservableCollection<CountdownItemViewModel>();

    /// <summary>
    /// 未完成的倒计时项的集合
    /// </summary>
    public ObservableCollection<CountdownItemViewModel> UnfinishedCountdowns { get; private set; }
        = new ObservableCollection<CountdownItemViewModel>();

    public DelegateCommand CreateCommand { get; private set; }

    public CountdownViewModel()
        : base(null)
    {
           
    }

    public CountdownViewModel(
        IDialogService dialog,
        IEventAggregator aggregator
    ) : base(aggregator)
    {
        this.ViewTitle = "倒计时";
        this.IsEmptyList = true;
        this.aggregator = aggregator;
        this.dialog = dialog;

        CreateCommand = new DelegateCommand(Create);
        LoadingItems();
    }

    public override void LoadingItems()
    {
        AddTestData();
    }

    private void Create()
    {
        dialog.ShowDialog(Helpers.Constants.COUNTDOWN_CREATE_DIALOG);
    }


    private void AddTestData()
    {
        UnfinishedCountdowns.Add(new CountdownItemViewModel()
        {
            Id = 1,
            Icon = Enum.GetName(PackIconKind.Leaf),
            Description = "秋至",
            Progress = 10.0,
            EndDate = DateTime.Now.AddDays(5),
            StartDate = DateTime.Now,
            IsStared = true,
            IsOver = false,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now
        });
        UnfinishedCountdowns.Add(new CountdownItemViewModel()
        {
            Id = 1,
            Icon = Enum.GetName(PackIconKind.Leaf),
            Description = "秋至",
            Progress = 20.0,

            EndDate = DateTime.Now.AddDays(5),
            StartDate = DateTime.Now,
            IsStared = true,
            IsOver = false,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now
        });
        UnfinishedCountdowns.Add(new CountdownItemViewModel()
        {
            Id = 1,
            Icon = Enum.GetName(PackIconKind.Leaf),
            Description = "秋至",
            Progress = 50.0,
            EndDate = DateTime.Now.AddDays(5),
            StartDate = DateTime.Now,
            IsStared = true,
            IsOver = false,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now
        });
        UnfinishedCountdowns.Add(new CountdownItemViewModel()
        {
            Id = 1,
            Icon = Enum.GetName(PackIconKind.Leaf),
            Progress = 0.0,
            Description = "秋至",
            EndDate = DateTime.Now.AddDays(5),
            StartDate = DateTime.Now,
            IsStared = true,
            IsOver = false,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now
        });

        FinishedCountdowns.Add(new CountdownItemViewModel()
        {
            Id = 1,
            Icon = Enum.GetName(PackIconKind.Leaf),
            Description = "秋至",
            EndDate = DateTime.Now.AddDays(5),
            StartDate = DateTime.Now,
            IsStared = true,
            Progress = 0.0,
            IsOver = false,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now
        });
        FinishedCountdowns.Add(new CountdownItemViewModel()
        {
            Id = 1,
            Icon = Enum.GetName(PackIconKind.Leaf),
            Description = "秋至",
            EndDate = DateTime.Now.AddDays(5),
            Progress = 0.0,
            StartDate = DateTime.Now,
            IsStared = true,
            IsOver = false,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now
        });
        FinishedCountdowns.Add(new CountdownItemViewModel()
        {
            Id = 1,
            Icon = Enum.GetName(PackIconKind.Leaf),
            Description = "秋至",
            Progress = 0.0,
            EndDate = DateTime.Now.AddDays(5),
            StartDate = DateTime.Now,
            IsStared = true,
            IsOver = false,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now
        });
    }
}