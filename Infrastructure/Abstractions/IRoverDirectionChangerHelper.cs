using Domain.Enums;

namespace Infrastructure.Abstractions;

public interface IRoverDirectionChangerHelper
{
    void ChangeRoverDirection(Turn turn);
}