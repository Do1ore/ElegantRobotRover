using Autofac;
using ElegantRobotRover.Extensions;
using ElegantRobotRover.Helpers;
using Infrastructure;
using Infrastructure.Abstractions;

var builder = new ContainerBuilder();

builder.ConfigureCustomServices();

var configuration = builder.AddConfiguration();

builder.AddSpaceStationHttpClient(configuration);

builder.ConfigureKafkaProducer(configuration);

var container = builder.Build();
await using var scope = container.BeginLifetimeScope();

var roverLocationService = scope.Resolve<IRoverLocationService>();
var kafka = scope.Resolve<KafkaProducer>();
while (true)
{
    await Task.Delay(1000);
    await kafka.ProduceAsync("rover", "Hello");
    ConsoleHelper.PrintActionsInfo();
    ConsoleHelper.ManageRoverCommands(roverLocationService, out var stop);
  
    if (stop) break;
}