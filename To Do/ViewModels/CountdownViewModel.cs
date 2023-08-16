using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using To_Do.Services;
using To_Do.Shared;
using To_Do.Shared.Paging;

namespace To_Do.ViewModels;

public class CountdownViewModel : BaseViewModel
{
    private readonly IDialogService dialog;
    private readonly IToDoApi service;

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
        IEventAggregator aggregator,
        IToDoApi service
    ) : base(aggregator)
    {
        this.ViewTitle = "倒计时";
        this.IsEmptyList = true;
        this.aggregator = aggregator;
        this.dialog = dialog;
        this.service = service;
        CreateCommand = new DelegateCommand(Create);
        LoadingItems();
    }

    public override async void LoadingItems()
    {
        var response = await service.GetAsync(new CountdownPagingDTO()
        {
            PageIndex = 0
        });

        if (response.IsSuccessStatusCode)
        {
            var dtos = response.Content;
            var now = DateTime.Now;
            foreach (var dto in dtos)
            {
                // finished
                if (now.Date > dto.EndDate.Date)
                {
                    FinishedCountdowns.Add(new CountdownItemViewModel(dto));
                }
                else
                {
                    UnfinishedCountdowns.Add(new CountdownItemViewModel(dto));
                }
            }
        }
    }

    private void Create()
    {
        dialog.ShowDialog(Helpers.Constants.COUNTDOWN_CREATE_DIALOG, result =>
        {
            switch (result.Result)
            {
                case ButtonResult.OK:
                    var dto = result.Parameters
                        .GetValue<CountdownDTO>("Countdown");
                    UnfinishedCountdowns.Add(new CountdownItemViewModel(dto));
                    break;
                default: 
                    break;
            }
        });
    }
}