using Domain.Enums;

namespace Infrastructure.Abstractions;

public interface ICommandExecutorHelperService
{
    
    void ExecuteMoveCommand(List<(Turn turn, int timesToExecute)> commands);
}