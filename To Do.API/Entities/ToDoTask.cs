namespace To_Do.API.Entities;

public class ToDoTask : TimeInfo
{
    public long TaskId { get; set; }
    public Guid UserId { get; set; }
    public int TaskType{ get; set; }
    public string TaskDescription { get; set; }
    public string TaskMemo { get; set; }
    public bool IsFinished { get; set; }
    public bool IsStared { get; set; }
}
