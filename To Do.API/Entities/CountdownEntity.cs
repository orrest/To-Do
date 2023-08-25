namespace To_Do.API.Entities;

public class CountdownEntity : TimeInfo
{
    public long CountdownId { get; set; }
    public Guid UserId { get; set; }

    public string Icon { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
