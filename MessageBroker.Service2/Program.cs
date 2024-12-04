using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();

factory.Uri = new("amqps://dylxmqal:7_A9Vfo3nd5WpN62rVmsltBJBzUrG6SU@cow.rmq2.cloudamqp.com/dylxmqal");

using IConnection connection = await factory.CreateConnectionAsync();
var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "direct-exchange-example", type: ExchangeType.Direct);

var name = await channel.QueueDeclareAsync();
var queueName = name.QueueName;

await channel.QueueBindAsync(
    queueName,
    exchange: "direct-exchange-example",
    routingKey: "direct-queue-example");

AsyncEventingBasicConsumer consumer = new(channel);

await channel.BasicConsumeAsync(queueName, autoAck: true, consumer);

consumer.ReceivedAsync += async (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();