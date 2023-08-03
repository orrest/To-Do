using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal abstract class ToDoBaseViewModel : BaseViewModel
{
    /// <summary>
    /// 当前视图的标题
    /// </summary>
    protected string viewTitle;
    public string ViewTitle
    {
        get { return viewTitle; }
        set { viewTitle = value; RaisePropertyChanged(); }
    }

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

    private bool isEmptyList;
    public bool IsEmptyList
    {
        get { return isEmptyList; }
        set { isEmptyList = value; RaisePropertyChanged(); }
    }

    private bool isLoading;
    public bool IsLoading
    {
        get { return isLoading; }
        set { isLoading = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<TaskViewModel> Tasks { get; private set; }
    public DelegateCommand DrawerOpenCommand { get; private set; }
    public DelegateCommand AddTaskCommand { get; private set; }


    protected TaskType taskType;
    private readonly IEventAggregator aggregator;
    protected readonly IToDoApi service;


    public ToDoBaseViewModel(
        string viewTitle, 
        IToDoApi service, 
        TaskType taskType,
        IEventAggregator aggregator
    ) : base(aggregator) 
    {
        drawerVms = new Dictionary<TaskViewModel, TaskDrawerViewModel>();

        Tasks = new ObservableCollection<TaskViewModel>();
        DrawerOpenCommand = new DelegateCommand(OpenDrawer);
        AddTaskCommand = new DelegateCommand(AddTask);

        this.viewTitle = viewTitle;
        this.service = service;
        this.taskType = taskType;
        this.aggregator = aggregator;

        IsLoading = true;
        IsEmptyList = false;

        LoadingTasks();
    }

    public async void LoadingTasks()
    {
        OpenLoading();

        var response = await service.GetAsync(new TaskPagingDTO()
        {
            TaskType = taskType,
            PageIndex = 0
        });

        if (response.IsSuccessStatusCode)
        {
            var tasks = response.Content;
            foreach (var dto in tasks)
            {
                Tasks.Insert(0, new TaskViewModel(service)
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

            IsLoading = false;
            IsEmptyList = Tasks.Count == 0;
        }
        else
        {
            aggregator.PublishMessage(viewTitle, response.Error?.Content);
        }

        CloseLoading();
    }

    private void OpenDrawer()
    {
        IsDrawerOpen = !IsDrawerOpen;

        if (SelectedTask != null)
        {
            var isContained = drawerVms.ContainsKey(SelectedTask);

            if (!isContained)
            {
                drawerVms[SelectedTask] = new TaskDrawerViewModel(
                    service,
                    aggregator,
                    SelectedTask,
                    () => { IsDrawerOpen = false; }, 
                    (task) => { Tasks.Remove(task); });
            }

            CurrentDrawer = drawerVms[SelectedTask];
        }
        else
        {
            throw new ApplicationException("Task not selected, this OpenDrawer method shouldn't be triggered");
        }
    }

    /// <summary>
    /// 添加一个新任务, 在VM中更新
    /// </summary>
    private async void AddTask()
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
            Tasks.Insert(0, new TaskViewModel(service)
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
        }
        else
        {
            // TODO snackbar mq
        }
    }
}