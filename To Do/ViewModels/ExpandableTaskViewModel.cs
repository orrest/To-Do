using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

// TODO refactor drawer and baseviewmodel(FetchItems)
public class ExpandableTaskViewModel : TaskViewModel
{
    private bool stepsInitialized = false;

    private bool isExpanded;
	public bool IsExpanded
	{
		get { return isExpanded; }
		set { isExpanded = value; RaisePropertyChanged(); }
	}

    public ObservableCollection<TaskStepViewModel> Steps { get; private set; }
        = new ObservableCollection<TaskStepViewModel>();

    public DelegateCommand ExpandCommand { get; private set; }

    public ExpandableTaskViewModel(
        IToDoApi service,
        IEventAggregator aggregator) : base(service, aggregator) 
    {
        ExpandCommand = new DelegateCommand(Expand);
    }

    public async void Expand()
    {
        IsExpanded = !IsExpanded;
        if (!stepsInitialized)
        {
            stepsInitialized = true;
            await FetchItems(0);
        }
    }

    public async Task FetchItems(int pageIndex)
    {
        //OpenLoading();

        var response = await service.GetAsync(
            new TaskStepPagingDTO()
            {
                PageIndex = pageIndex,
                TaskId = this.TaskId
            });

        if (response.IsSuccessStatusCode)
        {
            var steps = response.Content.Items;
            foreach (var dto in steps)
            {
                Steps.Add(new TaskStepViewModel(this, service, (step) => Steps.Remove(step), aggregator)
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
            //CloseLoading(steps.Count > 0);
        }
        else
        {
            aggregator.PublishMessage(this.TaskDescription, response.Error?.Content);
            //CloseLoading(false);
        }
    }
}
