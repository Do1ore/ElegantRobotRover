using Domain.DTOs;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class RoverLocation
{
    [Key]
    public int Id { get; set; }
    public long XPosition { get; set; }
    public long YPosition { get; set; }
    public bool IsAlive { get; set; }
    [EnumDataType(typeof(Direction))] public string? CurrentDirection { get; set; }
    public DateTime LastLocationDateTime { get; set; }

    public static implicit operator RoverLocation(RoverLocationDto roverLocation)
    {
        return new RoverLocation
        {
            XPosition = roverLocation.XPosition,
            YPosition = roverLocation.YPosition,
            CurrentDirection = roverLocation.CurrentDirection,
            LastLocationDateTime = roverLocation.LastLocationDateTime,
        };
    }
}
