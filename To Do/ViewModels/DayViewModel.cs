using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using To_Do.Models;

namespace To_Do.ViewModels;

public class DayViewModel : BindableBase
{
	public ObservableCollection<TaskModel> Tasks { get; set; } = new ObservableCollection<TaskModel>();

	private TaskModel? selectedTask;

	public TaskModel? SelectedTask
    {
		get { return selectedTask; }
		set { selectedTask = value; }
	}

	public DelegateCommand OpenDetailCommand { get; private set; }

	public DayViewModel()
	{
		OpenDetailCommand = new DelegateCommand(OpenDetail);
		FakeDataHelper.CreateTasks(Tasks);
	}

    private void OpenDetail()
    {
		
    }
}