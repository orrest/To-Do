using AutoMapper;
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
    #region Properties
    private TaskViewModel? selectedTask;
    private TaskStepViewModel? currentSelectedStep;
    private bool isDrawerOpen;
    private string taskDescriptionText = "";
    private ObservableCollection<TaskStepViewModel> currentTaskSteps = new ObservableCollection<TaskStepViewModel>();
    protected string viewTitle;
    protected TaskType taskType;
    protected readonly IMapper mapper;
    private readonly IEventAggregator aggregator;
    protected readonly IToDoApi service;
    private bool isEmptyList;
    private bool isLoading;

    /// <summary>
    /// 任务步骤的缓存, 实际操作这个集合
    /// </summary>
    public Dictionary<long, ObservableCollection<TaskStepViewModel>?> TaskStepsDic { get; private set; } = new Dictionary<long, ObservableCollection<TaskStepViewModel>?>();
    /// <summary>
    /// TODO 应该是一个只读的, 每次增删后从TaskStepsDic赋新值, RaisePropertyChanged
    /// </summary>
    public ObservableCollection<TaskViewModel> Tasks { get; private set; } = new ObservableCollection<TaskViewModel>();
    /// <summary>
    /// 每次切换任务后, 显示当前的Steps
    /// </summary>
    public ObservableCollection<TaskStepViewModel> CurrentTaskSteps
    {
        get { return currentTaskSteps; }
        set { currentTaskSteps = value; RaisePropertyChanged(); }
    }

    public DelegateCommand<TaskViewModel> DrawerOpenCommand { get; private set; }
    public DelegateCommand DrawerCloseCommand { get; private set; }
    public DelegateCommand FinishTaskCommand { get; private set; }
    public DelegateCommand StarTaskCommand { get; private set; }
    public DelegateCommand AddTaskCommand { get; private set; }
    public DelegateCommand DeleteTaskCommand { get; private set; }
    public DelegateCommand UpdateTaskCommand { get; private set; }
    public DelegateCommand<string> AddStepCommand { get; private set; }
    public DelegateCommand DeleteStepCommand { get; private set; }

    public string ViewTitle
	{
		get { return viewTitle; }
		set { viewTitle = value; RaisePropertyChanged(); }
	}

	public TaskViewModel? SelectedTask
    {
		get { return selectedTask; }
		set { selectedTask = value; RaisePropertyChanged(); }
	}

    public TaskStepViewModel? CurrentSelectedStep
    {
        get {
            System.Diagnostics.Debug.WriteLine($"{currentSelectedStep?.StepDescription}");
            return currentSelectedStep; }
        set { currentSelectedStep = value; 
            System.Diagnostics.Debug.WriteLine($"{currentSelectedStep?.StepDescription}");

            RaisePropertyChanged(); }
    }

    /// <summary>
    /// TODO Consider when drawer close, update the task and the steps.
    /// </summary>
    public bool IsDrawerOpen
	{
		get { return isDrawerOpen; }
		set { isDrawerOpen = value; RaisePropertyChanged(); }
	}
    
    public string InputTaskDescriptionText
    {
        get { return taskDescriptionText; }
        set { taskDescriptionText = value; RaisePropertyChanged(); }
    }

    public bool IsEmptyList
    {
        get { return isEmptyList; }
        set { isEmptyList = value; RaisePropertyChanged(); }
    }

    public bool IsLoading
    {
        get { return isLoading; }
        set { isLoading = value; RaisePropertyChanged(); }
    }
    #endregion

    public ToDoBaseViewModel(
        string viewTitle, 
        IToDoApi service, 
        TaskType taskType,
        IMapper mapper,
        IEventAggregator aggregator
    ) : base(aggregator) 
    {
        this.viewTitle = viewTitle;
        this.service = service;
        this.taskType = taskType;
        this.mapper = mapper;
        this.aggregator = aggregator;
        FinishTaskCommand = new DelegateCommand(Finish);
        StarTaskCommand = new DelegateCommand(Star);
        DrawerOpenCommand = new DelegateCommand<TaskViewModel>(OpenDrawer);
        DrawerCloseCommand = new DelegateCommand(CloseDrawer);
        AddTaskCommand = new DelegateCommand(AddTask);
        DeleteTaskCommand = new DelegateCommand(DeleteTask);
        UpdateTaskCommand = new DelegateCommand(UpdateTask);
        AddStepCommand = new DelegateCommand<string>(AddStep);
        DeleteStepCommand = new DelegateCommand(DeleteStep);

        this.IsLoading = true;
        this.IsEmptyList = false;

        Initialize();
    }

    /// <summary>
    /// When open the view, loading the tasks.
    /// </summary>
    public async void Initialize()
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
            var vms = mapper.Map<IList<TaskDTO>, IList<TaskViewModel>>(tasks!);
            Tasks.AddRange(vms);

            IsLoading = false;
            IsEmptyList = Tasks.Count == 0;
        }
        else
        {
            aggregator.PublishMessage(viewTitle, response.Error?.Content);
        }

        CloseLoading();
    }

    private void Finish()
    {
		SelectedTask!.IsFinished = !SelectedTask.IsFinished;
    }

    private void Star()
    {
		SelectedTask!.IsStared = !SelectedTask.IsStared;
    }

    private async void OpenDrawer(TaskViewModel task)
    {
        SelectedTask = task;
        IsDrawerOpen = !IsDrawerOpen;

        var selectedId = SelectedTask.TaskId;
        if (!TaskStepsDic.ContainsKey(selectedId)
            || TaskStepsDic[selectedId] == null
            || TaskStepsDic[selectedId]!.Count == 0)
        {
            var response = await service.GetStepsAsync(new TaskStepPagingDTO()
            {
                PageIndex = 0,
                TaskId = selectedId
            });

            if (!response.IsSuccessStatusCode)
            {
                // TODO message event
            }

            var steps = response.Content;
            if (steps.Count == 0)
            {
                // TODO show empty steps contorl
            }

            var stepVms = mapper.Map<IList<TaskStepDTO>, IList<TaskStepViewModel>>(steps);
            CurrentTaskSteps = new ObservableCollection<TaskStepViewModel>(stepVms);
            TaskStepsDic[selectedId] = CurrentTaskSteps;
        }
        else
        {
            CurrentTaskSteps = TaskStepsDic[selectedId];
        }
    }

    private void CloseDrawer()
    {
        IsDrawerOpen = false;
    }

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
            var tskVm = mapper.Map<TaskDTO, TaskViewModel>(response.Content);
            InputTaskDescriptionText = "";
            Tasks.Add(tskVm);
            TaskStepsDic[tskVm.TaskId].Add(new TaskStepViewModel() { StepOrder = 1 });
        }
        else
        {
            // TODO snackbar mq
        }
    }

    private async void DeleteTask()
    {
        var response = await service.DeleteAsync(selectedTask.TaskId);
        if (response.IsSuccessStatusCode)
        {
            Tasks.Remove(selectedTask);
            SelectedTask = null;
            IsDrawerOpen = false;
        }
        else
        {
            // TODO snackbar mq
        }
    }

    private async void UpdateTask()
    {
        var response = await service.UpdateAsync(new TaskDTO()
        {
            TaskId = SelectedTask.TaskId,
            TaskDescription = SelectedTask.TaskDescription,
            TaskMemo = SelectedTask.TaskMemo,
            CreateTime = SelectedTask.CreateTime,
            UpdateTime = SelectedTask.UpdateTime,
            TaskType = SelectedTask.TaskType,
            IsFinished = SelectedTask.IsFinished,
            IsStared = SelectedTask.IsStared
        });
        if (response.IsSuccessStatusCode)
        {

        }
        else
        {

        }
    }

    private async void AddStep(string step)
    {
        var taskId = SelectedTask!.TaskId;

        var response = await service.AddStepAsync(new TaskStepDTO()
        {
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            TaskId = taskId,
            StepDescription = step,
            StepOrder = CurrentTaskSteps.Count + 1,
            IsFinished = false
        });

        if (response.IsSuccessStatusCode)
        {
            var stepVm = mapper.Map<TaskStepViewModel>(response.Content);
            CurrentTaskSteps.Add(stepVm);
        }
        else
        {
            // TODO
        }
    }

    private async void DeleteStep()
    {
        if (CurrentSelectedStep == null)
        {
            return;
        }
        var stepId = CurrentSelectedStep.StepId;
        var response = await service.DeleteStepAsync(stepId);
        if (response.IsSuccessStatusCode)
        {
            CurrentTaskSteps.Remove(CurrentSelectedStep);
        }
        else
        {

        }
    }

}