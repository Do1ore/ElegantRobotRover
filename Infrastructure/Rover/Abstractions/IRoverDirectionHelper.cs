using Domain.Enums;

namespace Infrastructure.Rover.Abstractions;

/// <summary>
/// Helps with compass directions
/// </summary>
public interface IRoverDirectionHelper
{
    /// <summary>
    /// Changes rover direction by <see cref="Turn"/>. 90 degree left or right
    /// </summary>
    /// <param name="turn"><see cref="Turn"/></param>
    void ChangeRoverDirection(Turn turn);
}