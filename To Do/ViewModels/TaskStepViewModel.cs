using Prism.Mvvm;
using System;

namespace To_Do.ViewModels;

internal class TaskStepViewModel : BindableBase
{
    public long StepId { get; set; }
    public long TaskId { get; set; }

    private string stepDescription;
    private int stepOrder;
    private bool isFinished;

    public string StepDescription
    {
        get { return stepDescription; }
        set { stepDescription = value; RaisePropertyChanged(); }
    }

    public int StepOrder
    {
        get { return stepOrder; }
        set { stepOrder = value; RaisePropertyChanged(); }
    }

    public bool IsFinished
    {
        get { return isFinished; }
        set { isFinished = value; RaisePropertyChanged(); }
    }
}
