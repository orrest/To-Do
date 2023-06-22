namespace To_Do.Shared;

public class ToDoTaskAddingDTO
{
    public int TaskType { get; set; }
    public string TaskDescription { get; set; }
    public string TaskMemo { get; set; }
    public bool IsFinished { get; set; }
    public bool IsStared { get; set; }
}
