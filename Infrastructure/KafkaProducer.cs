using System.Diagnostics;
using Confluent.Kafka;

namespace Infrastructure;

public class KafkaProducer
{
    private readonly IProducer<Null, string> _producer;

    public KafkaProducer(ProducerConfig producerConfig)
    {
        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public async Task ProduceAsync(string topic, string message)
    {
        var deliveryReport = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
        Debug.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
    }
}