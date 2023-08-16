namespace Infrastructure.Rover.Abstractions;

public interface IRoverHttpClient
{
    Task<(int x, int y)> SendCurrentPositionAsync();
    Task GetLastPosition();
}