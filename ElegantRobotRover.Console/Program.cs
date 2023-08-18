using Autofac;
using ElegantRobotRover;
using Infrastructure.Rover.Abstractions;
using Microsoft.Extensions.Configuration;


var builder = new ContainerBuilder();

builder.ConfigureCustomServices();

var configuration = builder.AddConfiguration();

builder.AddSpaceStationHttpClient(configuration);

var container = builder.Build();
using var scope = container.BeginLifetimeScope();
var roverLocationService = scope.Resolve<IRoverLocationService>();

roverLocationService.SetPosition(1, 1, "N");
roverLocationService.Move("r1r2r3");

var httpClientService = scope.Resolve<IRoverHttpClientService>();
var result = await httpClientService.GetLastPosition();