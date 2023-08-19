using Domain.Entities;
using Domain.Enums;
using Infrastructure.Rover.Abstractions;
using Infrastructure.Rover.Implementation;

namespace Tests.Services;

public class CommandExecutorHelperServiceTests
{
    private readonly RobotRover _rover;
    private readonly IRoverDirectionHelper _direction;
    private readonly CommandExecutorHelper _commandExecutor;

    public CommandExecutorHelperServiceTests()
    {
        _rover = new RobotRover { XPosition = 10, YPosition = 10, CurrentDirection = Direction.North };
        _direction = Substitute.For<IRoverDirectionHelper>();
        var httpClientService = Substitute.For<IRoverHttpClientService>();
        _commandExecutor = new CommandExecutorHelper(_rover, _direction, httpClientService);
    }

    [Fact]
    public void ExecuteMoveCommand_Forward_MoveRoverCalledWithCorrectCommand()
    {
        //Arrange
        var expectedResult = new RobotRover { XPosition = 10, YPosition = 11, CurrentDirection = Direction.North };

        var commands = new List<(Turn turn, int timesToExecute)>
        {
            (Turn.Forward, 1)
        };
        //Act
        _commandExecutor.ExecuteMoveCommand(commands);
        //Assert
        Assert.Equal(expectedResult, _rover);
    }

    [Fact]
    public void ExecuteMoveCommand_Left_MoveRoverCalledWithCorrectCommand()
    {
        //Arrange
        var expectedResult = new RobotRover { XPosition = 6, YPosition = 10, CurrentDirection = Direction.West };

        _direction.When(a =>
            a.ChangeRoverDirection(Turn.Left)).Do(a => { _rover.CurrentDirection = Direction.West; });

        var commands = new List<(Turn turn, int timesToExecute)>
        {
            (Turn.Left, 4)
        };
        //Act
        _commandExecutor.ExecuteMoveCommand(commands);
        //Assert
        Assert.Equal(expectedResult, _rover);
    }

    [Fact]
    public void ExecuteMoveCommand_Right_MoveRoverCalledWithCorrectCommand()
    {
        //Arrange
        var expectedResult = new RobotRover { XPosition = 13, YPosition = 10, CurrentDirection = Direction.East };

        _direction.When(a =>
            a.ChangeRoverDirection(Turn.Left)).Do(a => { _rover.CurrentDirection = Direction.East; });

        var commands = new List<(Turn turn, int timesToExecute)>
        {
            (Turn.Left, 3)
        };
        //Act
        _commandExecutor.ExecuteMoveCommand(commands);
        //Assert
        Assert.Equal(expectedResult, _rover);
    }
}