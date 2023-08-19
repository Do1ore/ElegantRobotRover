using Domain.Entities;
using Domain.Enums;
using Infrastructure.Rover.Abstractions;
using Infrastructure.Rover.Implementation;
using Microsoft.Extensions.Configuration;

namespace Tests.Services;

public class RoverLocationServiceTests
{
    private readonly RobotRover _robotRover;
    private readonly IRoverCommandInterpreterHelper _commandInterpreterHelper;
    private readonly ICommandExecutorHelper _commandExecutor;
    private readonly RoverLocationService _roverLocationService;

    public RoverLocationServiceTests()
    {
        _robotRover = new RobotRover();
        _commandInterpreterHelper = Substitute.For<IRoverCommandInterpreterHelper>();
        _commandExecutor = Substitute.For<ICommandExecutorHelper>();
        var configuration = Substitute.For<IConfiguration>();

        var httpClientService = Substitute.For<IRoverHttpClientService>();
        _roverLocationService =
            new RoverLocationService(_robotRover, _commandInterpreterHelper, _commandExecutor, httpClientService,
                configuration);
    }

    [Fact]
    public void SetPosition_ValidInput_SetsPositionAndDirection()
    {
        // Arrange
        _commandInterpreterHelper.InterpretDirection("N").Returns(Direction.North);

        // Act
        _roverLocationService.StartRover();

        // Assert
        Assert.Equal(1, _robotRover.XPosition);
        Assert.Equal(2, _robotRover.YPosition);
        Assert.Equal(Direction.North, _robotRover.CurrentDirection);
    }

    [Fact]
    public async Task Move_ValidCommands_CallsExecutor()
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
    }

    [Fact]
    public void SetPosition_InvalidDirection_ThrowsException()
    {
        // Arrange
        _commandInterpreterHelper.InterpretDirection(Arg.Any<string>())
            .Returns(x => { throw new ArgumentException(); });

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _roverLocationService.StartRover());
    }

    [Fact]
    public void Move_InvalidCommands_ThrowsException()
    {
        // Arrange
        var commands = "L1R2";
        _commandInterpreterHelper.InterpretCommand(commands).Returns(x => { throw new ArgumentException(); });

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _roverLocationService.Move(commands));
    }
}