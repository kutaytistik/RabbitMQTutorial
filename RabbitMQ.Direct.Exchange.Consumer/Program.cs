using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//1.Adım
channel.ExchangeDeclare(exchange: "direct-exchange-example", ExchangeType.Direct);

//2.Adım
string queueName = channel.QueueDeclare().QueueName;

//3.Adım
channel.QueueBind(
    queue: queueName,
    exchange: "direct-exchange-example",
    routingKey: "direct-queue-example");

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.ReadLine();
