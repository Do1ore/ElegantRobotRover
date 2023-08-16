using Domain.Enums;

namespace Infrastructure.Rover.Abstractions;

public interface ICommandExecutorHelper
{
    
    void ExecuteMoveCommand(List<(Turn turn, int timesToExecute)> commands);
}