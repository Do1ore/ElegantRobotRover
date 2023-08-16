using Autofac;
using Domain.Entities;
using Infrastructure.Rover.Abstractions;
using Infrastructure.Rover.Implementation;

var builder = new ContainerBuilder();

builder.RegisterType<RobotRover>();

builder.RegisterType<RoverLocationService>().As<IRoverLocationService>().InstancePerLifetimeScope();
builder.RegisterType<RoverCommandInterpreterHelper>().As<IRoverCommandInterpreterHelper>().InstancePerLifetimeScope();
builder.RegisterType<RoverDirectionHelper>().As<IRoverDirectionChangerHelper>().InstancePerLifetimeScope();
builder.RegisterType<CommandHelperService>().As<ICommandExecutorHelperService>().InstancePerLifetimeScope();


var container = builder.Build();
using var scope = container.BeginLifetimeScope();
var roverLocationService = scope.Resolve<IRoverLocationService>();


roverLocationService.SetPosition(1, 1, "N");
roverLocationService.Move("r1r2r3");