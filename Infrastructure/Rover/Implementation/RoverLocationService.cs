using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Rover.Abstractions;

namespace Infrastructure.Rover.Implementation;

public class RoverLocationService : IRoverLocationService
{
    private readonly RobotRover _robotRover;
    private readonly IRoverCommandInterpreterHelper _commandInterpreterHelper;
    private readonly ICommandExecutorHelper _commandExecutor;
    private readonly IRoverHttpClientService _httpClientService;

    public RoverLocationService(RobotRover robotRover,
        IRoverCommandInterpreterHelper commandInterpreterHelper,
        ICommandExecutorHelper commandExecutor,
        IRoverHttpClientService httpClientService)
    {
        _robotRover = robotRover;
        _commandInterpreterHelper = commandInterpreterHelper;
        _commandExecutor = commandExecutor;
        _httpClientService = httpClientService;
    }

    public void SetPosition(int x, int y, string direction)
    {
        _robotRover.XPosition = x;
        _robotRover.YPosition = y;
        var directionEnum = _commandInterpreterHelper.InterpretDirection(direction);
        _robotRover.CurrentDirection = directionEnum;

        _httpClientService.SendCurrentPosition(new RoverLocationDto
        {
            XPosition = x,
            YPosition = y,
            CurrentDirection = direction,
            LastLocationDateTime = DateTime.UtcNow,
        });

        
    }

    public void Move(string commands)
    {
        var tuplesCommands = _commandInterpreterHelper.InterpretCommand(commands);
        _commandExecutor.ExecuteMoveCommand(tuplesCommands.ToList());

        _httpClientService.SendCurrentPosition(new RoverLocationDto
        {
            XPosition = _robotRover.XPosition,
            YPosition = _robotRover.YPosition,
            CurrentDirection = _robotRover.CurrentDirection.ToString(),
            LastLocationDateTime = DateTime.UtcNow,
        });
        Console.WriteLine(_robotRover.ToString());
    }
}