//RabbitMQ.Client paketini yükle
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// 1- RabbitMQ sunucusuna bağlantı oluştur
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("amqps://diliqtpi:niyo7iEW6vq3_5VZ3B64h0Dx1ekomA-O@fish.rmq.cloudamqp.com/diliqtpi");

// 2- Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// 3- Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);
//Consumerdaki kuyruk publisher işe birebir aynı yapılandırmada tanımlanmalıdır

// 4- Queue'dan Mesaj okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);
consumer.Received += (sender, e) =>
{
    // Kuyruğa gelen mesajın işlendiği yerdir
    // e.Body:Kuyruktaki mesajın verisini bütünsel olarak getirecektir
    // e.Body.Span veya e.Body.ToArray() kuyruktaki mesajın byte verisini getirecektir

    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.ReadLine();
