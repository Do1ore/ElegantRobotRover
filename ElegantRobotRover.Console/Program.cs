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

roverLocationService.SetPosition(1, 1, "W");
roverLocationService.Move("R1R3R3");
roverLocationService.Move("l16");

var httpClientService = scope.Resolve<IRoverHttpClientService>();
var result = httpClientService.GetLastPosition();