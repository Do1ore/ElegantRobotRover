using Domain.Enums;

namespace Infrastructure.Rover.Abstractions;

/// <summary>
/// Helps translate input to usable form
/// <seealso cref="Turn"/>
/// </summary>
public interface IRoverCommandInterpreterHelper
{
    ///  <summary>
    ///  Translate movement command to usable forms
    ///  </summary>
    ///  <param name="command">
    ///  Command string in the form "L1R2..."
    ///  </param>
    ///  <returns>
    ///   IEnumerable of tuple, where each tuple contains (<see cref="Turn"/> turn,  <see cref="int"/> executionTimes)
    ///  </returns>
    IEnumerable<(Turn turn, int executionTimes)> InterpretCommand(string command);

    /// <summary>
    /// Translate direction command to usable form
    /// </summary>
    /// <param name="direction">
    /// direction to move. Examples: North or N, South or S</param>
    /// <returns>
    ///<see cref="Direction"/>
    /// </returns>
    Direction InterpretDirection(string direction);
}