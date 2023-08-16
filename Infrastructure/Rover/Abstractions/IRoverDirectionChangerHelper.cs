using Domain.Enums;

namespace Infrastructure.Rover.Abstractions;

public interface IRoverDirectionChangerHelper
{
    void ChangeRoverDirection(Turn turn);
}