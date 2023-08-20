using Autofac;
using Confluent.Kafka;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Abstractions.Helpers;
using Infrastructure.Implementation;
using Infrastructure.Implementation.Helpers;
using Microsoft.Extensions.Configuration;

namespace ElegantRobotRover.Extensions;

public static class ContainerBuilderExtensions
{
    public static void ConfigureCustomServices(this ContainerBuilder builder)
    {
        builder.RegisterType<RobotRover>().SingleInstance();

        builder.RegisterType<RoverLocationService>().As<IRoverLocationService>().InstancePerLifetimeScope();

        builder.RegisterType<RoverCommandInterpreterHelper>().As<IRoverCommandInterpreterHelper>()
            .InstancePerLifetimeScope();

        builder.RegisterType<RoverDirectionHelper>().As<IRoverDirectionHelper>().InstancePerLifetimeScope();

        builder.RegisterType<CommandExecutorHelper>().As<ICommandExecutorHelper>().InstancePerLifetimeScope();

        builder.RegisterType<RoverHttpClientService>().As<IRoverHttpClientService>().InstancePerLifetimeScope();
    }

    public static IConfiguration AddConfiguration(this ContainerBuilder builder)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = configurationBuilder.Build();

        builder.RegisterInstance(config).As<IConfiguration>().SingleInstance();
        return config;
    }

    public static void AddSpaceStationHttpClient(this ContainerBuilder builder, IConfiguration configuration)
    {
        var baseUrl = configuration.GetSection("DefaultSpaceStationUrl").Value ??
                      throw new ArgumentException("DefaultSpaceStationUrl not found");

        builder.RegisterInstance(new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        }).SingleInstance();
    }

    public static void ConfigureKafkaProducer(this ContainerBuilder builder, IConfiguration configuration)
    {
        var bootstrapServers = configuration.GetSection("Kafka")["BootstrapServers"] ??
                               throw new ArgumentException("BootstrapServers not found");

        var producerConfig = new ProducerConfig { BootstrapServers = bootstrapServers };

        builder.RegisterType<KafkaProducer>()
            .WithParameter("producerConfig", producerConfig)
            .SingleInstance();
    }
}