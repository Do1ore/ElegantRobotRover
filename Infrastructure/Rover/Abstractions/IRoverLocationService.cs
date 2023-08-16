namespace Infrastructure.Rover.Abstractions;
public interface IRoverLocationService
{
    void SetPosition(int x, int y, string direction);
    void Move(string commands);
}