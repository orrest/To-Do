namespace To_Do.Shared.Paging;

public class TaskPagingDTO : PagingBase
{
    public Guid? UserId { get; set; }
    public TaskType TaskType { get; set; }
}
