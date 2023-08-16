using Domain.Enums;

namespace Infrastructure.Abstractions;

public interface IRoverCommandInterpreterHelper
{
    IEnumerable<(Turn turn, int executionTimes)> InterpretCommand(string command);
    Direction InterpretDirection(string direction);
}