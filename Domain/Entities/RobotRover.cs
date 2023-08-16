namespace Domain.Entities;

public class RobotRover
{
    public required long XPosition { get; init; }
    public required long YPosition { get; init; }
    public bool IsAlive { get; set; }
}