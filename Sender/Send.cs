using System.Runtime.Serialization.Json;
using System.Text;
using RabbitMQ.Client;

namespace Sender;

public enum Channels
{
    Kanal1,
    Kanal2,
    Kanal3,
    Kanal4,
}
public class Send
{
    public static async Task Main(string metin,Channels kanal)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "ryuka",
            Password = "123",
        };
        await using var connection = await factory.CreateConnectionAsync();

        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: kanal.ToString(),
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        
        var message = metin + "  ,RabbitMQ!";
        var body = Encoding.UTF8.GetBytes(message);


        // (6) Mesaj özelliklerini oluşturuyoruz
        var props = new BasicProperties()
        {
            ContentType = "text/plain",
            // ContentEncoding = "utf-8",
            DeliveryMode = DeliveryModes.Transient
        };
        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: "SelamKuyruk",
            mandatory: false,
            basicProperties: props,
            body: body
        );
        
        Console.WriteLine($" {message} Gönderildi: ");
    }
}