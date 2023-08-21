using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared.Paging;

namespace To_Do.ViewModels;

public class StaredViewModel : BaseViewModel
{
    private readonly IToDoApi service;

    public ObservableCollection<TaskViewModel> Tasks { get; private set; }
        = new ObservableCollection<TaskViewModel>();

    public StaredViewModel(
        IToDoApi service,
        IEventAggregator aggregator) : base(aggregator)
    {
        this.service = service;
        InitFetch();
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

            Tasks.Clear();
            var tasks = page.Items;
            foreach (var dto in tasks)
            {
                Tasks.Add(new TaskViewModel(service, aggregator)
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
            aggregator.PublishMessage(viewTitle, $"刷新成功");
        }
        else
        {
            aggregator.PublishMessage(viewTitle, $"加载数据失败 {response.StatusCode}");
            CloseLoading(false);
        }
    }

    public override async void InitFetch()
    {
        await FetchItems(PagingButtonsViewModel.FIRST_PAGE);
    }
}