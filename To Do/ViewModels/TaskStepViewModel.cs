using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

public class TaskStepViewModel : BindableBase
{
    #region Data
    public long StepId { get; set; }

    public long TaskId { get; set; }

    private string stepDescription = "";
    public string StepDescription
    {
        get { return stepDescription; }
        set { stepDescription = value; RaisePropertyChanged(); }
    }

    private int stepOrder;
    public int StepOrder
    {
        get { return stepOrder; }
        set { stepOrder = value; RaisePropertyChanged(); }
    }

    private bool isFinished;
    public bool IsFinished
    {
        get { return isFinished; }
        set { isFinished = value; RaisePropertyChanged(); }
    }

    private DateTime updateTime;
    public DateTime UpdateTime
    {
        get { return updateTime; }
        set { updateTime = value; RaisePropertyChanged(); }
    }

    private DateTime createTime;
    public DateTime CreateTime
    {
        get { return createTime; }
        set { createTime = value; RaisePropertyChanged(); }
    }
    #endregion

    /// <summary>
    /// Step所属的Task
    /// </summary>
    private TaskViewModel task;
    private readonly IToDoApi service;
    private readonly Action<TaskStepViewModel> deleteAction;
    private readonly IEventAggregator aggregator;

    public DelegateCommand DeleteStepCommand { get; private set; }
    public DelegateCommand UpdateStepCommand { get; private set; }

    public TaskStepViewModel(
        TaskViewModel task,
        IToDoApi service,
        Action<TaskStepViewModel> deleteAction, 
        IEventAggregator aggregator)
    {
        this.task = task;
        this.service = service;
        this.deleteAction = deleteAction;
        this.aggregator = aggregator;
        DeleteStepCommand = new DelegateCommand(Delete);
        UpdateStepCommand = new DelegateCommand(Update);
    }

    public TaskStepViewModel()
    {
    }

    private async void Delete()
    {
        var response = await service.DeleteStepAsync(StepId);
        if (response.IsSuccessStatusCode)
        {
            deleteAction.Invoke(this);
        }
    }

    /// <summary>
    /// Triggered when 
    /// </summary>
    private async void Update()
    {
        var res = await UpdateStep();
        if (!res)
        {
            aggregator.PublishMessage("TaskStepViewModel", 
                $"Step#{StepId} 更新失败");
        }
        Keyboard.ClearFocus();
    }

    private async Task<bool> UpdateStep()
    {
        var response = await service.UpdateStepAsync(new TaskStepDTO()
        {
            StepId = StepId,
            TaskId = TaskId,
            StepDescription = StepDescription,
            StepOrder = StepOrder,
            IsFinished = IsFinished,
            UpdateTime = DateTime.Now
        });
        return response.IsSuccessStatusCode;
    }
}
