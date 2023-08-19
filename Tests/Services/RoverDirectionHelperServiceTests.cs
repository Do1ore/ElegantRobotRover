using Domain.Entities;
using Domain.Enums;
using Infrastructure.Implementation;
using Infrastructure.Implementation.Helpers;

namespace Tests.Services;

public class RoverDirectionHelperServiceTests
{
    private readonly RobotRover _rover = new()
    {
        CurrentDirection = Direction.West
    };

    private readonly RoverDirectionHelper _sut;

    public RoverDirectionHelperServiceTests()
    {
        _sut = new RoverDirectionHelper(_rover);
    }

    [Fact]
    public void RoverDirectionHelper_TurnLeft_WithCorrectTurnDirection()
    {
        //Arrange
        var expectedResult = Direction.South;
        //Act
        _sut.ChangeRoverDirection(Turn.Left);
        //Assert
        Assert.Equal(expectedResult, _rover.CurrentDirection);
    }

    [Fact]
    public void RoverDirectionHelper_TurnRight_WithCorrectTurnDirection()
    {
        //Arrange
        var expectedResult = Direction.North;
        //Act
        _sut.ChangeRoverDirection(Turn.Right);
        //Assert
        Assert.Equal(expectedResult, _rover.CurrentDirection);
    }
}