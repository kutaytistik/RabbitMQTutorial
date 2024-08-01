using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers);

Console.WriteLine("Lütfen Header Value Değerini Giriniz:");
string value = Console.ReadLine();


string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName,
    exchange: "header-exchange-example",
    routingKey: string.Empty,
    new Dictionary<string, object>
    {
        //default değer any
        ["x-match"] = "all",
        ["no"] = value
    });

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
