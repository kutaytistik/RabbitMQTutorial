using RabbitMQ.Client;
using System.Text;


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://diliqtpi:niyo7iEW6vq3_5VZ3B64h0Dx1ekomA-O@fish.rmq.cloudamqp.com/diliqtpi");


using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", ExchangeType.Direct);

while (true)
{
    Console.WriteLine("Mesaj : ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "direct-exchange-example",
        routingKey: "direct-queue-example",
        body: byteMessage);
}

Console.ReadLine();