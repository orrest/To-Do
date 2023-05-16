using System.Collections.Generic;

namespace To_Do.Models;

public class TaskModel : BaseModel
{
    public string Description { get; set; }
    public bool IsStared { get; set; }
    public bool IsFinished { get; set; }
    public List<string> Steps { get; set; }
    public TaskCategory Category { get; set; }
}