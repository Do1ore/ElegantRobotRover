using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.DTOs;

public class RoverLocationDto
{
    public long XPosition { get; set; }
    public long YPosition { get; set; }
    public bool IsAlive { get; set; }
    [EnumDataType(typeof(Direction))] public string? CurrentDirection { get; set; }
    public DateTime LocationDateTime { get; set; }
    
}