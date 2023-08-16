using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

public class CountdownCreateDialogViewModel : BindableBase, IDialogAware
{
    private readonly IToDoApi service;
    private readonly IEventAggregator aggregator;

    public string Title => "CountdownCreateDialog";

    public event Action<IDialogResult>? RequestClose;

    /// <summary>
    /// 可选的图标
    /// </summary>
    public ObservableCollection<string> Icons { get; private set; }
        = new ObservableCollection<string>();

    /// <summary>
    /// 被选择的图标
    /// </summary>
    private string selectedIcon;
    public string SelectedIcon
    {
        get { return selectedIcon; }
        set { selectedIcon = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 描述这个倒计时项
    /// </summary>
    private string description;
    public string Description
    {
        get { return description; }
        set { description = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 日期的选择方式
    /// </summary>
    public ObservableCollection<string> DatePicks { get; private set; }
        = new ObservableCollection<string>();

    /// <summary>
    /// 日期的选择方式
    /// </summary>
    private string selectedDatePick;
    public string SelectedDatePick
    {
        get { return selectedDatePick; }
        set { selectedDatePick = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 创建日期
    /// </summary>
    private DateTime createDate;
    public DateTime CreateDate
    {
        get { return createDate; }
        set { createDate = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 目标日期
    /// </summary>
    private DateTime targetDate;
    public DateTime TargetDate
    {
        get { return targetDate; }
        set { targetDate = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 需要计时的天数
    /// </summary>
    private int days;
    public int Days
    {
        get { return days; }
        set 
        { 
            days = value;
            TargetDate = CreateDate.AddDays(days);
            RaisePropertyChanged(); 
        }
    }

    /// <summary>
    /// 关闭本dialog
    /// </summary>
    public DelegateCommand CancelCommand { get; private set; }

    /// <summary>
    /// 添加一条倒计时
    /// </summary>
    public DelegateCommand AddCountdownCommand { get; private set; }

    /// <summary>
    /// 为设计时DataContext
    /// </summary>
    public CountdownCreateDialogViewModel()
    {
        Description = "";

        InitIcons();
        InitDatePicks();
    }

    public CountdownCreateDialogViewModel(
        IToDoApi service,
        IEventAggregator aggregator
        )
    {
        AddCountdownCommand = new DelegateCommand(AddCountdown);
        CancelCommand = new DelegateCommand(CancelDialog);
        
        CreateDate = DateTime.Now;
        TargetDate = DateTime.Now;

        this.service = service;
        this.aggregator = aggregator;
        Description = "";

        InitIcons();
        InitDatePicks();
    }

    private void InitIcons()
    {
        Icons.Add(PackIconKind.BookClock.ToString());
        Icons.Add(PackIconKind.BookHeart.ToString());
        Icons.Add(PackIconKind.BookInformationVariant.ToString());
        Icons.Add(PackIconKind.BookCancel.ToString());
        SelectedIcon = Icons[0];
    }

    private void InitDatePicks()
    {
        DatePicks.Add(Helpers.Constants.COUNTDOWN_DATEPICK_DATE);
        DatePicks.Add(Helpers.Constants.COUNTDOWN_DATEPICK_DAYS);
        SelectedDatePick = Helpers.Constants.COUNTDOWN_DATEPICK_DAYS;
    }

    private async void AddCountdown()
    {
        var response = await service.AddAsync(new CountdownDTO()
        {
            CreateTime = CreateDate,
            UpdateTime = CreateDate,
            Icon = SelectedIcon,
            Description = Description,
            StartDate = CreateDate,
            EndDate = TargetDate
        });

        if (response.IsSuccessStatusCode)
        {
            var dto = new DialogParameters
            {
                { "Countdown", response.Content }
            };
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dto));
        }

        aggregator.PublishMessage("Countdown", $"{response.IsSuccessStatusCode}, " +
            $"{response.Content}");
    }

    private void CancelDialog()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
    }

    public bool CanCloseDialog()
    {
        return true;
    }

    public void OnDialogClosed()
    {
        
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {

    }
}
