namespace Domain.Entities;

public class Surface
{
    public int XLength { get; init; } = 40;
    public int YLength { get; init; } = 30;
    
    public List<RobotRover> RoversOnSurface = new();
    
}