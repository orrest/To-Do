using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using To_Do.Models;

namespace To_Do.ViewModels;

public class DayViewModel : BindableBase
{
	public ObservableCollection<TaskViewModel> Tasks { get; set; } = new ObservableCollection<TaskViewModel>();

	private TaskViewModel? selectedTask;

	public TaskViewModel? SelectedTask
    {
		get { return selectedTask; }
		set { selectedTask = value; }
	}

	public DelegateCommand<TaskViewModel> FinishCommand { get; private set; }
	public DelegateCommand OpenDetailCommand { get; private set; }
	public DelegateCommand<TaskViewModel> StarCommand { get; private set; }

	public DayViewModel()
	{
		FinishCommand = new DelegateCommand<TaskViewModel>(Finish);
		OpenDetailCommand = new DelegateCommand(OpenDetail);
		StarCommand = new DelegateCommand<TaskViewModel>(Star);
		FakeDataHelper.CreateTasks(Tasks);
	}

    private void Finish(TaskViewModel task)
    {
		task.IsFinished = !task.IsFinished;
    }

    private void OpenDetail()
    {
		
    }

    private void Star(TaskViewModel task)
    {
		task.IsStared = !task.IsStared;
    }
}