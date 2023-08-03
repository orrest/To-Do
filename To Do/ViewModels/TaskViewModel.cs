using Prism.Commands;
using Prism.Mvvm;
using System;
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

    public DateTime UpdateTime
    {
        get { return updateTime; }
        set { updateTime = value; RaisePropertyChanged(); }
    }

    public TaskType TaskType { get; set; }
    #endregion

    public DelegateCommand FinishTaskCommand { get; private set; }
    public DelegateCommand StarTaskCommand { get; private set; }
    public DelegateCommand UpdateTaskCommand { get; private set; }

    public TaskViewModel()
    {
        
    }

    public TaskViewModel(IToDoApi service)
    {
        FinishTaskCommand = new DelegateCommand(Finish);
        StarTaskCommand = new DelegateCommand(Star);
        UpdateTaskCommand = new DelegateCommand(UpdateTask);
        this.service = service;
    }

    private void Finish()
    {
        IsFinished = !IsFinished;
    }

    private void Star()
    {
        IsStared = !IsStared;
    }

    private async void UpdateTask()
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
        if (response.IsSuccessStatusCode)
        {

        }
        else
        {

        }
    }
}
