using Domain.DTOs;

namespace Infrastructure.Rover.Abstractions;

public interface IRoverHttpClientService
{
    void SendCurrentPosition(RoverLocationDto locationDto);
    RoverLocationDto GetLastPosition();
}