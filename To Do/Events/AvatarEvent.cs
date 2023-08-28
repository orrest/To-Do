using Prism.Events;

namespace To_Do.Events;

public class AvatarEvent : PubSubEvent<AvatarInfo>
{
}

public class AvatarInfo
{
    public string Color { get; set; }
    public string Kind { get; set; }
    public string Email { get; set; }

    public AvatarInfo(string color, string kind, string userName)
    {
        Color = color;
        Kind = kind;
        Email = userName;
    }
}
