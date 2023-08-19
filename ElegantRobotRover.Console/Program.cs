using Autofac;
using ElegantRobotRover;
using ElegantRobotRover.Helpers;
using Infrastructure.Rover.Abstractions;

var builder = new ContainerBuilder();

builder.ConfigureCustomServices();

var configuration = builder.AddConfiguration();

builder.AddSpaceStationHttpClient(configuration);

var container = builder.Build();
using var scope = container.BeginLifetimeScope();

var roverLocationService = scope.Resolve<IRoverLocationService>();
var httpClientService = scope.Resolve<IRoverHttpClientService>();

var result = httpClientService.GetLastPosition();

Console.WriteLine("Last position from api: {0}", result);
ConsoleHelper.PrintActionsInfo();
while (true)
{
    ConsoleHelper.ManageRoverCommands(roverLocationService);  
}



