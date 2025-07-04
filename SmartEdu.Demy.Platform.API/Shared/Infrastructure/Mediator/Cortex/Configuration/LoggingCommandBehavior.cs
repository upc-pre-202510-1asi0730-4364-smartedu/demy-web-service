using Cortex.Mediator.Commands;

namespace SmartEdu.Demy.Platform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

/// <summary>
/// Pipeline behavior for logging command execution.
/// </summary>
/// <typeparam name="TCommand">The type of command being handled.</typeparam>
public class LoggingCommandBehavior<TCommand>
    : ICommandPipelineBehavior<TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Handles the command by performing logging before and after execution.
    /// </summary>
    /// <param name="command">The command instance.</param>
    /// <param name="next">Delegate to invoke the next handler in the pipeline.</param>
    /// <param name="ct">Cancellation token.</param>
    public async Task Handle(
        TCommand command,
        CommandHandlerDelegate next,
        CancellationToken ct)
    {
        // Log before/after
        await next();
    }
}