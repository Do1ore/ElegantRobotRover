using Domain.Enums;

namespace Domain.Entities;

public record RobotRover
{
    public long XPosition { get; set; }
    public long YPosition { get; set; }
    public bool IsAlive { get; set; } = true;
    public Direction CurrentDirection { get; set; }
}