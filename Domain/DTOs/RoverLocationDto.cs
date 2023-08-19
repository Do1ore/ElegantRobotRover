using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Domain.DTOs;

public class RoverLocationDto
{
    public long XPosition { get; set; }
    public long YPosition { get; set; }
    
    public bool IsAlive { get; set; }
    [EnumDataType(typeof(Direction))] public string? CurrentDirection { get; set; }
    public DateTime LastLocationDateTime { get; set; }

    public static implicit operator RoverLocationDto(RoverLocation roverLocation)
    {
        return new RoverLocationDto
        {
            XPosition = roverLocation.XPosition,
            YPosition = roverLocation.YPosition,
            CurrentDirection = roverLocation.CurrentDirection,
            LastLocationDateTime = roverLocation.LastLocationDateTime,
        };
    }
}