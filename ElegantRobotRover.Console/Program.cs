using Autofac;
using ElegantRobotRover.Extensions;
using ElegantRobotRover.Helpers;
using Infrastructure.Abstractions;
using Microsoft.Extensions.Hosting;

var builder = new ContainerBuilder();

builder.ConfigureCustomServices();

var configuration = builder.AddConfiguration();

builder.AddSpaceStationHttpClient(configuration);
builder.ConfigureKafkaProducer(configuration);

var container = builder.Build();
await using var scope = container.BeginLifetimeScope();

var roverLocationService = scope.Resolve<IRoverLocationService>();
var kafkaBackgroundService = scope.Resolve<IHostedService>();
kafkaBackgroundService.StartAsync(CancellationToken.None);
while (true)
{
    ConsoleHelper.PrintActionsInfo();
    ConsoleHelper.ManageRoverCommands(roverLocationService, out var stop);

    if (stop) break;
}