using Domain.DTOs;

namespace Infrastructure.Rover.Abstractions;

public interface IRoverHttpClientService
{
    Task SendCurrentPositionAsync(RoverLocationDto locationDto);
    Task<RoverLocationDto> GetLastPosition();
}