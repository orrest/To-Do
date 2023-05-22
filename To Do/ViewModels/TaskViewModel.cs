using Prism.Mvvm;
using System.Collections.Generic;
using To_Do.Models;

namespace To_Do.ViewModels;

public class TaskViewModel : BindableBase
{
    public string Description { get; set; }
    
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


    public List<string> Steps { get; set; }
    public TaskCategory Category { get; set; }
}