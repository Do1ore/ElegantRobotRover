using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Rover.Abstractions;

namespace Infrastructure.Rover.Implementation;

public class CommandExecutorHelper : ICommandExecutorHelper
{
    private readonly RobotRover _rover;
    private readonly IRoverHttpClientService _httpClientService;
    private readonly IRoverDirectionHelper _direction;


    public CommandExecutorHelper(RobotRover rover,
        IRoverDirectionHelper direction, IRoverHttpClientService httpClientService)
    {
        _rover = rover;
        _direction = direction;
        _httpClientService = httpClientService;
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
                    _direction.ChangeRoverDirection(Turn.Left);
                    MoveRover(_rover.CurrentDirection, command.timesToExecute);
                    break;

                case Turn.Right:
                    _direction.ChangeRoverDirection(Turn.Right);
                    MoveRover(_rover.CurrentDirection, command.timesToExecute);
                    break;

                default: throw new ArgumentException("Invalid command");
            }

            Console.WriteLine(_rover.ToString());
        }

        _httpClientService.SendCurrentPosition(_rover);
    }

    private void MoveRover(Direction direction, int count)
    {
        switch (direction)
        {
            case Direction.North:
                _rover.YPosition += count;
                break;
            case Direction.East:
                _rover.XPosition += count;
                break;
            case Direction.South:
                _rover.YPosition -= count;
                break;
            case Direction.West:
                _rover.XPosition -= count;
                break;
            default:
                throw new ArgumentException("Something wrong");
        }
    }
}