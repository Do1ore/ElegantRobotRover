using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions;

namespace Infrastructure.Implementation;

public class CommandHelperService : ICommandExecutorHelperService
{
    private readonly RobotRover _rover;
    private readonly IRoverDirectionChangerHelper _directionChanger;

    public CommandHelperService(RobotRover rover,
        IRoverDirectionChangerHelper directionChanger)
    {
        _rover = rover;
        _directionChanger = directionChanger;
    }

    public void ExecuteMoveCommand(List<(Turn turn, int timesToExecute)> commands)
    {
        foreach (var command in commands)
        {
            switch (command.turn)
            {
                case Turn.Forward:
                    MoveRover(_rover.CurrentDirection, command.timesToExecute);
                    break;

                case Turn.Left:
                    _directionChanger.ChangeRoverDirection(Turn.Left);
                    MoveRover(_rover.CurrentDirection, command.timesToExecute);
                    break;

                case Turn.Right:
                    _directionChanger.ChangeRoverDirection(Turn.Right);
                    MoveRover(_rover.CurrentDirection, command.timesToExecute);
                    break;
                default: throw new ArgumentException("Invalid command");
            }

            Console.WriteLine(_rover.ToString());
        }
    }

    private void MoveRover(Direction direction, int count)
    {
        switch (direction)
        {
            case Direction.North:
                _rover!.YPosition += count;
                break;
            case Direction.East:
                _rover!.XPosition += count;
                break;
            case Direction.South:
                _rover!.YPosition -= count;
                break;
            case Direction.West:
                _rover!.XPosition -= count;
                break;
            default:
                throw new ArgumentException("Something wrong");
        }
    }
}