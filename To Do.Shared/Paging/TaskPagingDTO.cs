using To_Do.Shared.Paging;

namespace To_Do.Shared;

public class TaskPagingDTO : PagingBase
{
    public Guid UserId { get; set; }
    public TaskType TaskType { get; set; }
}
