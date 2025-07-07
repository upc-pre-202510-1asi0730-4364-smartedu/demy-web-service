using Cortex.Mediator.Notifications;

namespace SmartEdu.Demy.Platform.API.Shared.Domain.Model.Events;

/// <summary>
/// Defines a domain event within the application.
/// </summary>
/// <remarks>
/// This interface is used to identify classes as domain events that can be dispatched and processed by the event bus.
/// It inherits from <see cref="INotification"/> to support integration with the mediator pattern for event propagation.
/// </remarks>
public interface IEvent : INotification
{

}