using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();

factory.Uri = new("amqps://dylxmqal:7_A9Vfo3nd5WpN62rVmsltBJBzUrG6SU@cow.rmq2.cloudamqp.com/dylxmqal");

using IConnection connection =await factory.CreateConnectionAsync();
var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "direct-exchange-example", type: ExchangeType.Direct);

while (true)
{
    Console.Write("Mesaj : ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    await channel.BasicPublishAsync(exchange: "direct-exchange-example", routingKey: "direct-queue-example", body: byteMessage);
}
Console.Read();