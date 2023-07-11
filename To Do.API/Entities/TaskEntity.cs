using To_Do.Shared;

namespace To_Do.API.Entities;

public class TaskEntity : TimeInfo
{
    public long TaskId { get; set; }
    public Guid UserId { get; set; }
    public TaskType TaskType{ get; set; }
    public string TaskDescription { get; set; }
    public string TaskMemo { get; set; }
    public bool IsFinished { get; set; }
    public bool IsStared { get; set; }
}
