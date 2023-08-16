using Domain.Entities;
using Infrastructure.Rover.Abstractions;

namespace Infrastructure.Rover.Implementation;

public class RoverLocationService : IRoverLocationService
{
    private readonly RobotRover _robotRover;
    private readonly IRoverCommandInterpreterHelper _commandInterpreterHelper;
    private readonly ICommandExecutorHelperService _commandExecutor;

    public RoverLocationService(RobotRover robotRover,
        IRoverCommandInterpreterHelper commandInterpreterHelper,
        ICommandExecutorHelperService commandExecutor)
    {
        _robotRover = robotRover;
        _commandInterpreterHelper = commandInterpreterHelper;
        _commandExecutor = commandExecutor;
    }

    public void SetPosition(int x, int y, string direction)
    {
        _robotRover.XPosition = x;
        _robotRover.YPosition = y;
        var directionEnum = _commandInterpreterHelper.InterpretDirection(direction);
        _robotRover.CurrentDirection = directionEnum;
        Console.WriteLine(_robotRover.ToString());
    }

    public void Move(string commands)
    {
        var tuplesCommands = _commandInterpreterHelper.InterpretCommand(commands);
        _commandExecutor.ExecuteMoveCommand(tuplesCommands.ToList());
    }
}