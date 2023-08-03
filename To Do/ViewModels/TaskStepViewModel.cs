using Prism.Commands;
using Prism.Mvvm;
using System;
using To_Do.Services;

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

    public DelegateCommand DeleteStepCommand { get; private set; }

    public TaskStepViewModel(
        TaskViewModel task,
        IToDoApi service,
        Action<TaskStepViewModel> deleteAction)
    {
        this.task = task;
        this.service = service;
        this.deleteAction = deleteAction;
        DeleteStepCommand = new DelegateCommand(DeleteStep);
    }

    public TaskStepViewModel()
    {
    }

    private async void DeleteStep()
    {
        var response = await service.DeleteStepAsync(StepId);
        if (response.IsSuccessStatusCode)
        {
            deleteAction.Invoke(this);
        }
    }

    private void UpdateStep()
    {
    }
}
