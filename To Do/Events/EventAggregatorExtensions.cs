using Prism.Events;
using System;
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

    #region Loading event
    public static void PublishLoading(this IEventAggregator aggregator, bool isLoading)
    {
        aggregator.GetEvent<LoadingEvent>().Publish(isLoading);
    }

    public static void SubscribeLoading(this IEventAggregator aggregator, Action<bool> action)
    {
        aggregator.GetEvent<LoadingEvent>().Subscribe(action);
    }
    #endregion
}
