namespace To_Do.Shared;

public class CountdownDTO : TimeInfo
{
    public long Id { get; set; }
    public string Icon { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
