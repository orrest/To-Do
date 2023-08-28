using Prism.Events;

namespace To_Do.Events;

public class SyncInfoEvent : PubSubEvent<SyncInfo>
{

}

public class SyncInfo
{
    public string Color { get; set; }
    public string Info { get; set; }
    public SyncInfo(string color, string info)
    {
        Color = color;
        Info = info;
    }
}
