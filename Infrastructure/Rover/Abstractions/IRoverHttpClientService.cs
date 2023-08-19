using Domain.DTOs;

namespace Infrastructure.Rover.Abstractions;

/// <summary>
/// HttpClient to communicate with space station web api
/// </summary>
public interface IRoverHttpClientService
{
    /// <summary>
    /// Http post request to send latest rover movement to space station 
    /// </summary>
    /// <param name="locationDto"><see cref="RoverLocationDto"/></param>
    void SendCurrentPosition(RoverLocationDto locationDto);

    /// <summary>
    /// Http get request to fetch last rover location
    /// </summary>
    /// <returns><see cref="RoverLocationDto"/></returns>
    RoverLocationDto GetLastPosition();
}