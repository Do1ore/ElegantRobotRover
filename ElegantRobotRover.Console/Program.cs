using Autofac;
using ElegantRobotRover.Extensions;
using ElegantRobotRover.Helpers;
using Infrastructure.Abstractions;

var builder = new ContainerBuilder();

builder.ConfigureCustomServices();

var configuration = builder.AddConfiguration();

builder.AddSpaceStationHttpClient(configuration);

var container = builder.Build();
using var scope = container.BeginLifetimeScope();

var roverLocationService = scope.Resolve<IRoverLocationService>();

while (true)
{
    ConsoleHelper.PrintActionsInfo();
    ConsoleHelper.ManageRoverCommands(roverLocationService, out var stop);
    if (stop) break;
}