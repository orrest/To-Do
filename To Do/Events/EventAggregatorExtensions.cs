using MaterialDesignThemes.Wpf;
using Prism.Events;
using System;
using System.Drawing;
using To_Do.Events;

namespace To_Do.Helpers;

public static class EventAggregatorExtensions
{
    #region Message event
    public static void PublishMessage (this IEventAggregator aggregator, string viewName, string message)
    {
        aggregator.GetEvent<MessageEvent>().Publish($"{viewName} {message}");
    }

    public static void SubscribeMessage(this IEventAggregator aggregator, Action<string> action)
    {
        aggregator.GetEvent<MessageEvent>().Subscribe(action, ThreadOption.UIThread);
    }
    #endregion

    #region SyncInfo event
    public static void PublishSyncInfo(this IEventAggregator aggregator, string color, string info)
    {
        aggregator.GetEvent<SyncEvent>().Publish(new SyncInfo(color, info));
    }

    public static void SubscribeSyncInfo(this IEventAggregator aggregator, Action<SyncInfo> action)
    {
        aggregator.GetEvent<SyncEvent>().Subscribe(action, ThreadOption.UIThread);
    }
    #endregion

    #region AvatarInfo event
    public static void PublishAvatarInfo(this IEventAggregator aggregator, string email)
    {
        var seed = 0;
        foreach (var ch in email)
        {
            seed += ch;
        }
        var random = new Random(seed);
        var randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

        var capital = email[0];
        var kind = "";
        if (char.IsLetter(capital))
        {
            kind = $"Alpha{char.ToUpper(email[0])}";
        } 
        else if (char.IsDigit(capital))
        {
            kind = $"Numeric{char.ToUpper(email[0])}";
        }

        aggregator.GetEvent<AvatarEvent>()
            .Publish(new AvatarInfo($"#{randomColor.Name}", kind, email));
    }

    public static void SubscribeAvatarInfo(this IEventAggregator aggregator, Action<AvatarInfo> action)
    {
        aggregator.GetEvent<AvatarEvent>().Subscribe(action, ThreadOption.UIThread);
    }
    #endregion

    #region
    public static void PublishStartupNavigation(this IEventAggregator aggregator)
    {
        aggregator.GetEvent<NavigationEvent>().Publish();
    }

    public static void SubscribeStartupNavigation(this IEventAggregator aggregator, Action action)
    {
        aggregator.GetEvent<NavigationEvent>().Subscribe(action, ThreadOption.UIThread);
    }
    #endregion
}
