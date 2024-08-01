//RabbitMQ.Client paketini yükle

using RabbitMQ.Client;
using System.Text;

// 1- RabbitMQ sunucusuna bağlantı oluştur
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new("");

// 2- Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// 3- Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

// 4- Queue'ya Mesaj gönderme
// RabbitMQ kuyruğa atacağı mesajları byte türünde kabul etmektedir mesajları byte dönüştürmek gerekiyor
//byte[] message = Encoding.UTF8.GetBytes("Merhaba");
//channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}

Console.ReadLine();
