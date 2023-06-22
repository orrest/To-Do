using Prism.Commands;
using System.Collections.ObjectModel;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal abstract class ToDoBaseViewModel : BaseViewModel
{
	private string viewTitle;
    private TaskType taskType;
    public readonly IToDoTaskService service;

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

	public DelegateCommand<TaskViewModel> FinishCommand { get; private set; }
    public DelegateCommand<TaskViewModel> DrawerOpenCommand { get; private set; }
    public DelegateCommand<TaskViewModel> StarCommand { get; private set; }
    public DelegateCommand DrawerCloseCommand { get; private set; }
    public DelegateCommand<string> AddTaskCommand { get; private set; }

    internal ToDoBaseViewModel(
        string viewTitle, IToDoTaskService service, TaskType taskType
    ) : base() 
    {
        this.viewTitle = viewTitle;
        this.service = service;
        this.taskType = taskType;

        FinishCommand = new DelegateCommand<TaskViewModel>(Finish);
        StarCommand = new DelegateCommand<TaskViewModel>(Star);
        DrawerOpenCommand = new DelegateCommand<TaskViewModel>(OpenDrawer);
        DrawerCloseCommand = new DelegateCommand(CloseDrawer);
        AddTaskCommand = new DelegateCommand<string>(AddTask);
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

    private async void AddTask(string taskDescription)
    {
        var response = await service.AddAsync(new ToDoTaskAddingDTO()
        {
            TaskType = taskType,
            TaskDescription = taskDescription,
            TaskMemo = "",
            IsFinished = false,
            IsStared = false
        });

        if (response.IsSuccessStatusCode)
        {

        }
        else
        {

        }
    }

}