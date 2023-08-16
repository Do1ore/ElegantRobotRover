using Domain.Enums;

namespace Infrastructure.Rover.Abstractions;

public interface IRoverCommandInterpreterHelper
{
    IEnumerable<(Turn turn, int executionTimes)> InterpretCommand(string command);
    Direction InterpretDirection(string direction);
}