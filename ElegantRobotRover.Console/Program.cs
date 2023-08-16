using Autofac;
using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Implementation;

var builder = new ContainerBuilder();

builder.RegisterType<RobotRover>().SingleInstance();

builder.RegisterType<RoverLocationService>().As<IRoverLocationService>().InstancePerLifetimeScope();
builder.RegisterType<RoverCommandInterpreterHelper>().As<IRoverCommandInterpreterHelper>().InstancePerLifetimeScope();
builder.RegisterType<RoverDirectionHelper>().As<IRoverDirectionChangerHelper>().InstancePerLifetimeScope();
builder.RegisterType<CommandHelperService>().As<ICommandExecutorHelperService>().InstancePerLifetimeScope();


var container = builder.Build();
using var scope = container.BeginLifetimeScope();
var roverLocationService = scope.Resolve<IRoverLocationService>();

roverLocationService.SetPosition(1, 1, "N");
roverLocationService.Move("R3L46434353454R3R1R0R045656456");