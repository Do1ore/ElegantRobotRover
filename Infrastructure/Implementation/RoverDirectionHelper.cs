using Domain.Entities;
using Domain.Enums;
using Infrastructure.Abstractions;

namespace Infrastructure.Implementation;

public class RoverDirectionHelper : IRoverDirectionChangerHelper
{
    private readonly RobotRover _rover;

    private readonly LinkedList<Direction> _directions = new(new[]
    {
        Direction.West, Direction.North, Direction.East, Direction.South
    });


    public RoverDirectionHelper(RobotRover rover)
    {
        _rover = rover;
    }

    public void ChangeRoverDirection(Turn turn)
    {
        var currentDirection = _rover.CurrentDirection;

        var directionNode = _directions.Find(currentDirection);

        if (directionNode is null)
        {
            throw new ArgumentException("Direction not found");
        }

        Direction direction = Direction.North;

        switch (turn)
        {
            case Turn.Left:
                if (directionNode.Previous is null || directionNode.List is null)
                {
                    direction = directionNode.List!.Last();
                }
                else
                {
                    direction = directionNode.Previous.Value;
                }

                break;
            case Turn.Right:
                if (directionNode.Next is null || directionNode.List is null)
                {
                    direction = directionNode.List!.First();
                }
                else
                {
                    direction = directionNode.Next.Value;
                }

                break;
            default: throw new ArgumentException("Invalid turn");
        }

        _rover.CurrentDirection = direction;
    }
}