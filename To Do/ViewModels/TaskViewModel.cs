using Prism.Mvvm;
using System;
using System.Collections.Generic;
using To_Do.Shared;

namespace To_Do.ViewModels;

internal class TaskViewModel : BindableBase
{
    private string taskDescription;

    public string TaskDescription
    {
        get { return taskDescription; }
        set { taskDescription = value; RaisePropertyChanged(); }
    }

    private string taskMemo;

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

    public DateTime UpdateTime
    {
        get { return updateTime; }
        set { updateTime = value; RaisePropertyChanged(); }
    }

    public List<string> Steps { get; set; }
    public TaskType TaskType { get; set; }
}
