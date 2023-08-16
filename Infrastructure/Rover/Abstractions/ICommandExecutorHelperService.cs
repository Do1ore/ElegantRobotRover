using Domain.Enums;

namespace Infrastructure.Rover.Abstractions;

public interface ICommandExecutorHelperService
{
    
    void ExecuteMoveCommand(List<(Turn turn, int timesToExecute)> commands);
}