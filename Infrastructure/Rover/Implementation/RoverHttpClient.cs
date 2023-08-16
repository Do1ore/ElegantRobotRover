using Infrastructure.Rover.Abstractions;

namespace Infrastructure.Rover.Implementation;

public class RoverHttpClient : IRoverHttpClient
{
    public Task<(int x, int y)> SendCurrentPositionAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetLastPosition()
    {
        throw new NotImplementedException();
    }
}