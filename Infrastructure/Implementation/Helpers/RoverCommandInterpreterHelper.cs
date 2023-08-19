using System.Text.RegularExpressions;
using Domain.Enums;
using Infrastructure.Abstractions.Helpers;

namespace Infrastructure.Implementation.Helpers;

public class RoverCommandInterpreterHelper : IRoverCommandInterpreterHelper
{
    private readonly Regex _commandPattern = new(@"[RL]\d|\d", RegexOptions.Compiled);

    public Direction InterpretDirection(string direction)
    {
        if (string.IsNullOrEmpty(direction))
        {
            throw new ArgumentException("Null or empty direction");
        }

        return direction.ToUpper() switch
        {
            "N" or "NORTH" => Direction.North,
            "E" or "EAST" => Direction.East,
            "W" or "WEST" => Direction.West,
            "S" or "SOUTH" => Direction.South,
            _ => throw new ArgumentException("Invalid direction")
        };
    }

    public IEnumerable<(Turn, int)> InterpretCommand(string command)
    {
        if (!IsCommandValid(command))
        {
            throw new ArgumentException("Command is not valid");
        }

        var matches = _commandPattern.Matches(command.ToUpper());
        List<(Turn, int)> commands = new List<(Turn, int)>();

        foreach (var match in matches.ToList())
        {
            Console.WriteLine(match);
            if (match.Value.ToUpper().First() == 'R')
            {
                var timesToMove = int.Parse(match.Value.Last().ToString());
                commands.Add(new(Turn.Right, timesToMove));
                continue;
            }

            if (match.Value.ToUpper().First() == 'L')
            {
                var timesToMove = int.Parse(match.Value.Last().ToString());
                commands.Add(new(Turn.Left, timesToMove));
                continue;
            }

            if (int.TryParse(match.Value, out var result))
            {
                commands.Add(new(Turn.Forward, result));
            }
            
        }

        foreach (var c in commands)
        {
            Console.WriteLine($"Command: Dir: {c.Item1}\tCount: {c.Item2}");
        }
        return commands;
    }

    private bool IsCommandValid(string command)
    {
        if (string.IsNullOrEmpty(command)) return false;

        return _commandPattern.IsMatch(command);
    }
}