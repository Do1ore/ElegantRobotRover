using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundWorkers;

public class KafkaBackgroundService : BackgroundService
{
    private readonly KafkaProducer _kafkaProducer;
    private readonly string _topic;
    private readonly int _delayTimeMs;
    private readonly string _defaultMessage;

    public KafkaBackgroundService(KafkaProducer kafkaProducer, IConfiguration configuration
    )
    {
        _kafkaProducer = kafkaProducer;

        _topic = configuration.GetSection("Kafka")["Topic"] ?? throw new ArgumentException("Topic not found");

        string delayTimeMsString = configuration.GetSection("Kafka")["DefaultMessageProduceIntervalMs"] ??
                                   throw new ArgumentException("DefaultMessageProduceIntervalMs not found");

        _defaultMessage = configuration.GetSection("Kafka")["DefaultMessage"] ??
                          throw new ArgumentException("DefaultMessage not found");


        _delayTimeMs = int.Parse(delayTimeMsString);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _kafkaProducer.ProduceAsync(_topic, _defaultMessage);
            Debug.WriteLine($"Message produced with topic: {_topic}, content: {_defaultMessage} ");
            await Task.Delay(_delayTimeMs, stoppingToken);
        }
    }
}