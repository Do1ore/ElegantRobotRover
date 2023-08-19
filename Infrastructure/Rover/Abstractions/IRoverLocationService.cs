namespace Infrastructure.Rover.Abstractions;
public interface IRoverLocationService
{
    void StartRover();
    void Move(string commands);
}