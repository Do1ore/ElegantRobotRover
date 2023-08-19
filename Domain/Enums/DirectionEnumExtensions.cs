namespace Domain.Enums;

public static class DirectionEnumExtensions
{
    public static string ToStringFast(this Direction value)
        => value switch
        {
            Direction.East => nameof(Direction.East),
            Direction.West => nameof(Direction.West),
            Direction.North => nameof(Direction.North),
            Direction.South => nameof(Direction.South),
            _ => value.ToString(),
        };
}