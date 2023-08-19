using Domain.Enums;

namespace Infrastructure.Abstractions.Helpers;

/// <summary>
/// Helper to execute commands
/// </summary>
public interface ICommandExecutorHelper
{
    /// <summary>
    /// Moves the rover as commanded
    /// </summary>
    /// <param name="commands">
    /// Commands in useful form
    /// </param>
    void ExecuteMoveCommand(List<(Turn turn, int timesToExecute)> commands);
}