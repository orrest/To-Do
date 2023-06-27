using AutoMapper;
using Prism.Commands;
using System.Collections.ObjectModel;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal abstract class ToDoBaseViewModel : BaseViewModel
{
    protected string viewTitle;
    protected TaskType taskType;
    protected readonly IMapper mapper;
    protected readonly IToDoTaskService service;

    public string ViewTitle
	{
		get { return viewTitle; }
		set { viewTitle = value; RaisePropertyChanged(); }
	}

	public ObservableCollection<TaskViewModel> Tasks { get; set; } = new ObservableCollection<TaskViewModel>();

	private TaskViewModel? selectedTask;

	public TaskViewModel? SelectedTask
    {
		get { return selectedTask; }
		set { selectedTask = value; RaisePropertyChanged(); }
	}

	private bool isDrawerOpen;

	public bool IsDrawerOpen
	{
		get { return isDrawerOpen; }
		set { isDrawerOpen = value; RaisePropertyChanged(); }
	}

    private string taskDescriptionText;

    public string TaskDescriptionText
    {
        get { return taskDescriptionText; }
        set { taskDescriptionText = value; RaisePropertyChanged(); }
    }

    public DelegateCommand<TaskViewModel> FinishCommand { get; private set; }
    public DelegateCommand<TaskViewModel> DrawerOpenCommand { get; private set; }
    public DelegateCommand<TaskViewModel> StarCommand { get; private set; }
    public DelegateCommand DrawerCloseCommand { get; private set; }
    public DelegateCommand AddTaskCommand { get; private set; }
    public DelegateCommand DeleteTaskCommand { get; private set; }
    public DelegateCommand UpdateTaskCommand { get; private set; }

    public ToDoBaseViewModel(
        string viewTitle, 
        IToDoTaskService service, 
        TaskType taskType,
        IMapper mapper
    ) : base() 
    {
        this.viewTitle = viewTitle;
        this.service = service;
        this.taskType = taskType;
        this.mapper = mapper;
        FinishCommand = new DelegateCommand<TaskViewModel>(Finish);
        StarCommand = new DelegateCommand<TaskViewModel>(Star);
        DrawerOpenCommand = new DelegateCommand<TaskViewModel>(OpenDrawer);
        DrawerCloseCommand = new DelegateCommand(CloseDrawer);
        AddTaskCommand = new DelegateCommand(AddTask);
        DeleteTaskCommand = new DelegateCommand(DeleteTask);
        UpdateTaskCommand = new DelegateCommand(UpdateTask);
	}

    private void Finish(TaskViewModel task)
    {
		task.IsFinished = !task.IsFinished;
    }

    private void Star(TaskViewModel task)
    {
		task.IsStared = !task.IsStared;
    }

    private void OpenDrawer(TaskViewModel task)
    {
        SelectedTask = task;
        IsDrawerOpen = !IsDrawerOpen;
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
            TaskDescription = TaskDescriptionText,
            TaskMemo = "",
            IsFinished = false,
            IsStared = false
        };

        var response = await service.AddAsync(task);

        if (response.IsSuccessStatusCode)
        {
            var tskVm = mapper.Map<TaskDTO, TaskViewModel>(response.Content);
            Tasks.Add(tskVm);
            TaskDescriptionText = "";
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
}