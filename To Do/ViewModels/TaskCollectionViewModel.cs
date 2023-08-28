using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;
using To_Do.Shared.Paging;

namespace To_Do.ViewModels;

internal abstract class TaskCollectionViewModel : PagingViewModel
{
    /// <summary>
    /// 鼠标选中的Task
    /// </summary>
    private TaskViewModel? selectedTask;
    public TaskViewModel? SelectedTask
    {
        get { return selectedTask; }
        set { selectedTask = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 新增Task时对Task的描述
    /// </summary>
    private string inputTaskDescriptionText = "";
    public string InputTaskDescriptionText
    {
        get { return inputTaskDescriptionText; }
        set { inputTaskDescriptionText = value; RaisePropertyChanged(); }
    }
    
    /// <summary>
    /// 是否打开抽屉
    /// </summary>
    private bool isDrawerOpen;
    public bool IsDrawerOpen
    {
        get { return isDrawerOpen; }
        set { isDrawerOpen = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// 当前打开的抽屉内容
    /// </summary>
    private TaskDrawerViewModel? currentDrawer;
    public TaskDrawerViewModel? CurrentDrawer
    {
        get { return currentDrawer; }
        set { currentDrawer = value; RaisePropertyChanged(); }
    }

    /// <summary>
    /// Drawer的ViewModel, 打开抽屉时动态初始化
    /// </summary>
    private Dictionary<TaskViewModel, TaskDrawerViewModel> drawerVms;
    public ObservableCollection<TaskViewModel> Tasks { get; private set; }
    public DelegateCommand DrawerOpenCommand { get; private set; }
    public DelegateCommand AddTaskCommand { get; private set; }

    private readonly IToDoApi service;
    protected TaskType taskType;

    public TaskCollectionViewModel() {  }

    public TaskCollectionViewModel(
        IToDoApi service,
        TaskType taskType,
        IEventAggregator aggregator
    ) : base(aggregator)
    {
        drawerVms = new Dictionary<TaskViewModel, TaskDrawerViewModel>();

        Tasks = new ObservableCollection<TaskViewModel>();
        DrawerOpenCommand = new DelegateCommand(OpenDrawer);
        AddTaskCommand = new DelegateCommand(AddTask);

        this.service = service;
        this.taskType = taskType;
        this.aggregator = aggregator;

        IsLoading = true;
        IsEmptyList = false;
    }

    public override async void InitFetch()
    {
        await FetchItems(PagingButtonsViewModel.FIRST_PAGE);
    }

    public override async Task FetchItems(int pageIndex)
    {
        OpenLoading();

        var response = await service.GetAsync(new TaskPagingDTO()
        {
            TaskType = taskType,
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
            aggregator.PublishMessage(ViewTitle, $"刷新成功");
        }
        else
        {
            aggregator.PublishMessage(ViewTitle, $"加载数据失败 {response.StatusCode}");
            CloseLoading(false);
        }
    }

    public void OpenDrawer()
    {
        IsDrawerOpen = !IsDrawerOpen;

        if (SelectedTask == null)
        {
            throw new ApplicationException("Task not selected, this OpenDrawer method shouldn't be triggered");
        }
        
        if (SelectedTask.Drawer != null)
        {
            return;
        }

        SelectedTask.Drawer = new TaskDrawerViewModel(
            service,
            aggregator,
            SelectedTask,
            () => { IsDrawerOpen = false; }, 
            (task) => { Tasks.Remove(task); });
    }

    public async void AddTask()
    {
        var task = new TaskDTO()
        {
            TaskType = taskType,
            TaskDescription = InputTaskDescriptionText,
            TaskMemo = "",
            IsFinished = false,
            IsStared = false
        };

        var response = await service.AddAsync(task);

        if (response.IsSuccessStatusCode)
        {
            var dto = response.Content;
            Tasks.Insert(0, new TaskViewModel(service, aggregator)
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
            InputTaskDescriptionText = "";
            IsEmptyList = false;
        }
        else
        {
            // TODO snackbar mq
        }
    }
}