using Autofac;
using ElegantRobotRover;
using Infrastructure.Rover.Abstractions;
using Microsoft.Extensions.Configuration;


var builder = new ContainerBuilder();

builder.ConfigureCustomServices();
builder.AddConfiguration();


var container = builder.Build();
using var scope = container.BeginLifetimeScope();
var roverLocationService = scope.Resolve<IRoverLocationService>();


roverLocationService.SetPosition(1, 1, "N");
roverLocationService.Move("r1r2r3");