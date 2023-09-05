using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;
using To_Do.Shared.Paging;
using To_Do.Views;

namespace To_Do.ViewModels;

public class CountdownViewModel : PagingViewModel
{
    private readonly IDialogService dialog;
    private readonly IToDoApi service;

    /// <summary>
    /// ����ɵĵ���ʱ��ļ���
    /// </summary>
    public ObservableCollection<CountdownItemViewModel> FinishedCountdowns { get; private set; }
        = new ObservableCollection<CountdownItemViewModel>();

    /// <summary>
    /// δ��ɵĵ���ʱ��ļ���
    /// </summary>
    public ObservableCollection<CountdownItemViewModel> UnfinishedCountdowns { get; private set; }
        = new ObservableCollection<CountdownItemViewModel>();

    public DelegateCommand CreateCommand { get; private set; }

    public override string ViewTitle => CountdownView.Title;

    public CountdownViewModel() {  }

    public CountdownViewModel(
        IDialogService dialog,
        IEventAggregator aggregator,
        IToDoApi service) : base(aggregator)
    {
        this.IsEmptyList = true;
        this.dialog = dialog;
        this.service = service;
        CreateCommand = new DelegateCommand(Create);
    }

    public override async void InitFetch()
    {
        await FetchItems(PagingButtonsViewModel.FIRST_PAGE);
    }

    public override async Task FetchItems(int pageIndex)
    {
        OpenLoading();
        var response = await service.GetAsync(new CountdownPagingDTO()
        {
            PageIndex = pageIndex
        });

        if (response.IsSuccessStatusCode)
        {
            FinishedCountdowns.Clear();
            UnfinishedCountdowns.Clear();

            var page = response.Content;
            PagingVm.SetPageInfo(page);
            
            var dtos = page.Items;
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
            CloseLoading(dtos.Count > 0);
        }
        else
        {
            aggregator.PublishMessage(ViewTitle, "��ȡ����ʧ��");
            CloseLoading(false);
        }
    }

    private void Create()
    {
        dialog.ShowDialog(nameof(CountdownCreateDialog), result =>
        {
            switch (result.Result)
            {
                case ButtonResult.OK:
                    var dto = result.Parameters
                        .GetValue<CountdownDTO>("Countdown");
                    UnfinishedCountdowns.Insert(0, new CountdownItemViewModel(dto));
                    break;
                default: 
                    break;
            }
        });
    }
}