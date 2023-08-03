using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using To_Do.Helpers;
using To_Do.Services;
using To_Do.Shared;

namespace To_Do.ViewModels;

public class TaskViewModel : BindableBase
{
    #region Data
    private long taskId;

    public long TaskId
    {
        get { return taskId; }
        set { taskId = value; RaisePropertyChanged(); }
    }

    private string taskDescription = "";

    public string TaskDescription
    {
        get { return taskDescription; }
        set { taskDescription = value; RaisePropertyChanged(); }
    }

    private string taskMemo = "";

    public string TaskMemo
    {
        get { return taskMemo; }
        set { taskMemo = value; RaisePropertyChanged(); }
    }

    private bool isStared;

    public bool IsStared
    {
        get { return isStared; }
        set { isStared = value; RaisePropertyChanged(); }
    }

    private bool isFinished;

    public bool IsFinished
    {
        get { return isFinished; }
        set { isFinished = value; RaisePropertyChanged(); }
    }

    private DateTime createTime;

    public DateTime CreateTime
    {
        get { return createTime; }
        set { createTime = value; RaisePropertyChanged(); }
    }

    private DateTime updateTime;
    private readonly IToDoApi service;
    private readonly IEventAggregator aggregator;

    public DateTime UpdateTime
    {
        get { return updateTime; }
        set { updateTime = value; RaisePropertyChanged(); }
    }

    public TaskType TaskType { get; set; }
    #endregion

    public DelegateCommand FinishTaskCommand { get; private set; }
    public DelegateCommand StarTaskCommand { get; private set; }

    public TaskViewModel()
    {
        
    }

    public TaskViewModel(
        IToDoApi service, 
        IEventAggregator aggregator)
    {
        FinishTaskCommand = new DelegateCommand(Finish);
        StarTaskCommand = new DelegateCommand(Star);
        this.service = service;
        this.aggregator = aggregator;
    }

    private async void Finish()
    {
        IsFinished = !IsFinished;
        var res = await UpdateTask();
        if (!res)
        {
            aggregator.PublishMessage("TaskViewModel", $"Task#{TaskId} 更新失败");
        }
    }

    private async void Star()
    {
        IsStared = !IsStared;
        var res = await UpdateTask();
        if (!res)
        {
            aggregator.PublishMessage("TaskViewModel", $"Task#{TaskId} 更新失败");
        }
    }

    private async Task<bool> UpdateTask()
    {
        var response = await service.UpdateAsync(new TaskDTO()
        {
            TaskId = TaskId,
            TaskDescription = TaskDescription,
            TaskMemo = TaskMemo,
            CreateTime = CreateTime,
            UpdateTime = UpdateTime,
            TaskType = TaskType,
            IsFinished = IsFinished,
            IsStared = IsStared
        });
        return response.IsSuccessStatusCode;
    }
}
