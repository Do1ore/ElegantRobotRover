using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Rover.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Rover.Implementation;

public class RoverLocationService : IRoverLocationService
{
    private readonly RobotRover _robotRover;
    private readonly IRoverCommandInterpreterHelper _commandInterpreterHelper;
    private readonly ICommandExecutorHelper _commandExecutor;
    private readonly IRoverHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public RoverLocationService(
        RobotRover robotRover,
        IRoverCommandInterpreterHelper commandInterpreterHelper,
        ICommandExecutorHelper commandExecutor,
        IRoverHttpClientService httpClientService,
        IConfiguration configuration
    )
    {
        _robotRover = robotRover;
        _commandInterpreterHelper = commandInterpreterHelper;
        _commandExecutor = commandExecutor;
        _httpClientService = httpClientService;
        _configuration = configuration;
    }

    public void StartRover()
    {
        RoverLocationDto roverLocation;
        try
        {
            roverLocation = _httpClientService.GetLastPosition();
        }
        catch (ApplicationException applicationException)
        {
            //if space station have no data about last rover location
            var (x, y, defaultDirection) = GetDefaultLocationValuesFromConfig();

            Console.WriteLine(applicationException.Message);
            _robotRover.XPosition = int.Parse(x);
            _robotRover.YPosition = int.Parse(y);
            _robotRover.CurrentDirection = _commandInterpreterHelper.InterpretDirection(defaultDirection);

            Console.WriteLine("Rover location initialized from config: {0}", _robotRover);
            return;
        }


        //if space station have data about last rover location
        _robotRover.XPosition = roverLocation.XPosition;
        _robotRover.YPosition = roverLocation.YPosition;
        _robotRover.CurrentDirection = _commandInterpreterHelper.InterpretDirection(roverLocation.CurrentDirection ??
            throw new InvalidOperationException("Current direction is not valid"));

        Console.WriteLine("Rover location read from SpaceStation: {0}", _robotRover);
    }


    public void Move(string commands)
    {
        var tuplesCommands = _commandInterpreterHelper.InterpretCommand(commands);
        _commandExecutor.ExecuteMoveCommand(tuplesCommands.ToList());

        _httpClientService.SendCurrentPosition(new RoverLocationDto
        {
            XPosition = _robotRover.XPosition,
            YPosition = _robotRover.YPosition,
            CurrentDirection = _robotRover.CurrentDirection.ToStringFast(),
            LastLocationDateTime = DateTime.UtcNow,
        });
        Console.WriteLine(_robotRover.ToString());
    }

    private (string x, string y, string defaultDirection) GetDefaultLocationValuesFromConfig()
    {
        var x = _configuration.GetSection("DefaultLocation")["X"] ??
                throw new ArgumentException("DefaultLocation for x not found");
        var y = _configuration.GetSection("DefaultLocation")["Y"] ??
                throw new ArgumentException("DefaultLocation for y not found");

        var defaultDirection = _configuration.GetSection("DefaultLocation")["Direction"] ??
                               throw new ArgumentException("Default location not found");
        return (x, y, defaultDirection);
    }
}