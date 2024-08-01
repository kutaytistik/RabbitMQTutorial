using RabbitMQ.Client;
using System.Text;


ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("");


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
