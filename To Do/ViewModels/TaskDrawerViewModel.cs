using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

public class TaskDrawerViewModel : BaseViewModel
{
    private readonly IToDoApi service;

    /// <summary>
    /// 当前Drawer所属的task
    /// </summary>
    private TaskViewModel task;
    private readonly Action closeDrawer;
    private readonly Action<TaskViewModel> deleteTaskAction;

    public TaskViewModel Task
    {
        get { return task; }
        set { task = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 当前选中的Step
    /// </summary>
    private TaskStepViewModel? currentSelectedStep;
    public TaskStepViewModel? CurrentSelectedStep
    {
        get { return currentSelectedStep; }
        set { currentSelectedStep = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 添加Step的描述
    /// </summary>
    private string inputStepDescription = "";
    public string InputStepDescription
    {
        get { return inputStepDescription; }
        set { inputStepDescription = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 当前抽屉显示的Step数据
    /// </summary>
    public ObservableCollection<TaskStepViewModel> Steps { get; private set; } 
        = new ObservableCollection<TaskStepViewModel>();

    public DelegateCommand CloseDrawerCommand { get; private set; }
    public DelegateCommand AddStepCommand { get; private set; }
    public DelegateCommand DeleteTaskCommand { get; private set; }

    public override string ViewTitle => "";

    public TaskDrawerViewModel() : base(null) {  }

    public TaskDrawerViewModel(
        IToDoApi service,
        IEventAggregator aggregator,
        TaskViewModel task,
        Action closeDrawer, 
        Action<TaskViewModel> deleteTaskAction) : base(aggregator)
    {
        this.task = task;
        this.closeDrawer = closeDrawer;
        this.deleteTaskAction = deleteTaskAction;
        this.service = service;
        CloseDrawerCommand = new DelegateCommand(closeDrawer);
        AddStepCommand = new DelegateCommand(AddStep);
        DeleteTaskCommand = new DelegateCommand(DeleteTask);

        InitFetch();
    }

    public override async void InitFetch()
    {
        await FetchItems(PagingButtonsViewModel.FIRST_PAGE);
    }

    public override async Task FetchItems(int pageIndex)
    {
        OpenLoading();

        var response = await service.GetAsync(
            new TaskStepPagingDTO()
            {
                PageIndex = 0,
                TaskId = task.TaskId
            });

        if (response.IsSuccessStatusCode)
        {
            var steps = response.Content.Items;
            foreach (var dto in steps)
            {
                Steps.Add(new TaskStepViewModel(task, service, (step) => Steps.Remove(step), aggregator)
                {
                    StepId = dto.StepId,
                    TaskId = dto.TaskId,
                    StepDescription = dto.StepDescription,
                    StepOrder = dto.StepOrder,
                    IsFinished = dto.IsFinished,
                    CreateTime = dto.CreateTime,
                    UpdateTime = dto.UpdateTime,
                });
            }
            CloseLoading(steps.Count > 0);
        }
        else
        {
            aggregator.PublishMessage(task.TaskDescription, response.Error?.Content);
            CloseLoading(false);
        }
    }

    /// <summary>
    /// 添加一个新的Step到数据库
    /// TODO 要不要把它放到dispatcher 闲时
    /// </summary>
    public async void AddStep()
    {
        var response = await service.AddStepAsync(new TaskStepDTO()
        {
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            TaskId = task.TaskId,
            StepDescription = InputStepDescription,
            StepOrder = Steps.Count,
            IsFinished = false
        });

        if (response.IsSuccessStatusCode)
        {
            var dto = response.Content;
            Steps.Add(new TaskStepViewModel(task, service, (step) => Steps.Remove(step), aggregator)
            {
                StepId = dto.StepId,
                TaskId = dto.TaskId,
                StepDescription = dto.StepDescription,
                StepOrder = dto.StepOrder,
                IsFinished = dto.IsFinished,
                CreateTime = dto.CreateTime,
                UpdateTime = dto.UpdateTime,
            });
            InputStepDescription = "";
        }
    }

    private async void DeleteTask()
    {
        var response = await service.DeleteAsync(Task.TaskId);
        if (response.IsSuccessStatusCode)
        {
            deleteTaskAction.Invoke(Task);
            closeDrawer.Invoke();
        }
        else
        {

        }
    }
}
