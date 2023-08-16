using Autofac;
using Domain.Entities;
using Infrastructure.Rover.Abstractions;
using Infrastructure.Rover.Implementation;
using Microsoft.Extensions.Configuration;

namespace ElegantRobotRover;

public static class ContainerBuilderExtensions
{
    public static void ConfigureCustomServices(this ContainerBuilder builder)
    {
        builder.RegisterType<RobotRover>();

        builder.RegisterType<RoverLocationService>().As<IRoverLocationService>().InstancePerLifetimeScope();
        builder.RegisterType<RoverCommandInterpreterHelper>().As<IRoverCommandInterpreterHelper>()
            .InstancePerLifetimeScope();
        builder.RegisterType<RoverDirectionHelper>().As<IRoverDirectionChangerHelper>().InstancePerLifetimeScope();
        builder.RegisterType<CommandHelperService>().As<ICommandExecutorHelperService>().InstancePerLifetimeScope();
    }

    public static void AddConfiguration(this ContainerBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = configurationBuilder.Build();

        builder.RegisterInstance(config).As<IConfiguration>().SingleInstance();
    }
}