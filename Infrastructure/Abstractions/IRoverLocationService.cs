using Infrastructure.Abstractions.Helpers;

namespace Infrastructure.Abstractions;

/// <summary>
/// Service to drive a rover
/// <remarks>
/// This is main service, that use others
/// </remarks>
/// <seealso cref="IRoverHttpClientService"/>
/// <seealso cref="IRoverDirectionHelper"/>
/// <seealso cref="IRoverCommandInterpreterHelper"/>
/// <seealso cref="ICommandExecutorHelper"/>
/// </summary>
public interface IRoverLocationService
{
    /// <summary>
    /// Start rover
    /// </summary>
    void StartRover();

    /// <summary>
    /// Moves rover by given command
    /// </summary>
    /// <param name="commands"></param>
    void Move(string commands);
}