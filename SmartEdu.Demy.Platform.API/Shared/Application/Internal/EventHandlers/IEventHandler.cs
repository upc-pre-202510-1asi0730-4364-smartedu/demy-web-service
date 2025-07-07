using Cortex.Mediator.Notifications;
using SmartEdu.Demy.Platform.API.Shared.Domain.Model.Events;

namespace SmartEdu.Demy.Platform.API.Shared.Application.Internal.EventHandlers;

/// <summary>
/// Defines a handler for domain events.
/// </summary>
/// <typeparam name="TEvent">The type of event to handle.</typeparam>
/// <remarks>
/// Extends INotificationHandler to integrate with the mediator pipeline for event publishing.
/// </remarks>
public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{

}