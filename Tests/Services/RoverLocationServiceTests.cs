using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions;
using Infrastructure.Abstractions.Helpers;
using Infrastructure.Implementation;
using Microsoft.Extensions.Configuration;
using NSubstitute.ExceptionExtensions;

namespace Tests.Services;

public class RoverLocationServiceTests
{
    private readonly RobotRover _robotRover;
    private readonly IRoverCommandInterpreterHelper _commandInterpreterHelper;
    private readonly ICommandExecutorHelper _commandExecutor;
    private readonly RoverLocationService _roverLocationService;
    private readonly IRoverHttpClientService _httpClientService;
    private readonly IConfiguration _configuration;

    public RoverLocationServiceTests()
    {
        _robotRover = new RobotRover { XPosition = 100 };
        _commandInterpreterHelper = Substitute.For<IRoverCommandInterpreterHelper>();
        _commandExecutor = Substitute.For<ICommandExecutorHelper>();
        _configuration = Substitute.For<IConfiguration>();

        _httpClientService = Substitute.For<IRoverHttpClientService>();
        _roverLocationService = new RoverLocationService(_robotRover, _commandInterpreterHelper,
            _commandExecutor, _httpClientService,_configuration);
    }

    [Fact]
    public void SetPosition_ValidInput_SetsPositionAndDirection()
    {
        // Arrange
        _commandInterpreterHelper.InterpretDirection("N").Returns(Direction.North);
        _httpClientService.GetLastPosition().Throws(new ApplicationException());
        _configuration.GetSection("DefaultLocation")["X"].Returns("1");
        _configuration.GetSection("DefaultLocation")["Y"].Returns("2");
        _configuration.GetSection("DefaultLocation")["Direction"].Returns("N");
        // Act
        _roverLocationService.StartRover();

        // Assert
        Assert.Equal(1, _robotRover.XPosition);
        Assert.Equal(2, _robotRover.YPosition);
        Assert.Equal(Direction.North, _robotRover.CurrentDirection);
    }

    [Fact]
    public Task Move_ValidCommands_CallsExecutor()
    {
        // Arrange
        var commands = "L1R2";
        var interpretedCommands = new List<(Turn turn, int timesToExecute)>
        {
            (Turn.Left, 1),
            (Turn.Right, 2)
        };
        _commandInterpreterHelper.InterpretCommand(commands).Returns(interpretedCommands);

        // Act
        _roverLocationService.Move(commands);

        // Assert
        _commandExecutor.Received(1).ExecuteMoveCommand(
            Arg.Is<List<(Turn turn, int timesToExecute)>>(c =>
                c.SequenceEqual(interpretedCommands)));
        return Task.CompletedTask;
    }


    [Fact]
    public void Move_InvalidCommands_ThrowsException()
    {
        // Arrange
        var commands = "L1R2";
        _commandInterpreterHelper.InterpretCommand(commands).Throws(new ArgumentException());

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _roverLocationService.Move(commands));
    }
}