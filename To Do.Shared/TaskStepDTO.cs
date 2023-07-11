﻿namespace To_Do.Shared;

public class TaskStepDTO : TimeInfo
{
    public long StepId { get; set; }
    public long TaskId { get; set; }
    public string StepDescription { get; set; }
    public int StepOrder { get; set; }
    public bool IsFinished { get; set; }
}
