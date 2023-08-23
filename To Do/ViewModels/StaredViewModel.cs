using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared.Paging;
using To_Do.Views;

namespace To_Do.ViewModels;

public class StaredViewModel : PagingViewModel
{
    private readonly IToDoApi service;

    public ObservableCollection<ExpandableTaskViewModel> StaredTasks { get; private set; }
        = new ObservableCollection<ExpandableTaskViewModel>();

    public override string ViewTitle => StaredView.Title;

    public StaredViewModel() {  }

    public StaredViewModel(
        IToDoApi service,
        IEventAggregator aggregator) : base(aggregator)
    {
        this.service = service;
    }

    public override async Task FetchItems(int pageIndex)
    {
        OpenLoading();

        var response = await service.GetStaredAsync(new PagingBase()
        {
            PageIndex = pageIndex
        });

        if (response.IsSuccessStatusCode)
        {
            var page = response.Content;

            PagingVm.SetPageInfo(page);

            StaredTasks.Clear();
            var tasks = page.Items;
            foreach (var dto in tasks)
            {
                StaredTasks.Add(new ExpandableTaskViewModel(service, aggregator)
                {
                    TaskId = dto.TaskId,
                    TaskDescription = dto.TaskDescription,
                    TaskMemo = dto.TaskMemo,
                    IsStared = dto.IsStared,
                    IsFinished = dto.IsFinished,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                    TaskType = dto.TaskType
                });
            }

            CloseLoading(tasks.Count > 0);
            aggregator.PublishMessage(ViewTitle, $"刷新成功");
        }
        else
        {
            aggregator.PublishMessage(ViewTitle, $"加载数据失败 {response.StatusCode}");
            CloseLoading(false);
        }
    }

    public override async void InitFetch()
    {
        await FetchItems(PagingButtonsViewModel.FIRST_PAGE);
    }
}