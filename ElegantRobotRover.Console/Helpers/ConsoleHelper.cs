using Infrastructure.Rover.Abstractions;

namespace ElegantRobotRover.Helpers;

public static class ConsoleHelper
{
    public static void ManageRoverCommands(IRoverLocationService roverLocationService)
    {
        var command = Console.ReadLine() ?? throw new ArgumentException("Command cannot be null");
        int.TryParse(command, out var intCommand);

        switch (intCommand)
        {
            case 1:
                roverLocationService.StartRover();
                break;
            case 2:
                Console.WriteLine(
                    "Enter rover commands to move like R1L24 where [R] - turn direction," +
                    " [1] - how many steps go to," +
                    " [single number] - go forward N times");

                var moveCommands = Console.ReadLine() ?? throw new ArgumentException("Commands cannot be null");
                roverLocationService.Move(moveCommands);

                break;
            case 0:
                break;
        }
    }

    public static void PrintActionsInfo()
    {
        Console.WriteLine("Press 1 to start rover");
        Console.WriteLine("Press 2 to move using commands like [R2L34]");
        Console.WriteLine("Press 0 to exit");
    }
}