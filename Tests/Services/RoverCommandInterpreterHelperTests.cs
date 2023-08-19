using System.Diagnostics.CodeAnalysis;
using Domain.Enums;
using Infrastructure.Implementation;
using Infrastructure.Implementation.Helpers;

namespace Tests.Services;

[SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
public class RoverCommandInterpreterHelperTests
{
    private readonly RoverCommandInterpreterHelper _sut = new();

    [Theory]
    [InlineData("N", Direction.North)]
    [InlineData("E", Direction.East)]
    [InlineData("W", Direction.West)]
    [InlineData("S", Direction.South)]
    
    [InlineData("North", Direction.North)]
    [InlineData("East", Direction.East)]
    [InlineData("West", Direction.West)]
    [InlineData("South", Direction.South)]
    
    [InlineData("north", Direction.North)]
    [InlineData("east", Direction.East)]
    [InlineData("west", Direction.West)]
    [InlineData("south", Direction.South)]
    
    [InlineData("nOrtH", Direction.North)]
    [InlineData("eAsT", Direction.East)]
    [InlineData("wEsT", Direction.West)]
    [InlineData("sOutH", Direction.South)]
    public void InterpretDirection_WithCorrectInput(string direction, Direction expected)
    {
        //Act
        var actual = _sut.InterpretDirection(direction);
        //Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData("wert")]
    [InlineData("Northh")]
    [InlineData("WESTt")]
    public void InterpretDirection_WithInvalidInput(string direction)
    {
        //Assert
        Assert.Throws<ArgumentException>(() => _sut.InterpretDirection(direction));
    }
}