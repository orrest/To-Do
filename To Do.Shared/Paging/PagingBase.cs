namespace To_Do.Shared.Paging;

public class PagingBase
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 20;
}
