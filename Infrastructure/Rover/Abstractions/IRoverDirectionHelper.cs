using Domain.Enums;

namespace Infrastructure.Rover.Abstractions;

public interface IRoverDirectionHelper
{
    void ChangeRoverDirection(Turn turn);
}