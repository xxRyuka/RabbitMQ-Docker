using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiver;

public class Receive
{
    public static async Task Main()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "SelamKuyruk",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        Console.WriteLine(" [*] Mesaj bekleniyor... Çıkmak için CTRL+C");

        var consumer = new AsyncEventingBasicConsumer(channel);
        
        
        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Alındı: {message}");

            await channel.BasicAckAsync(ea.DeliveryTag, false);
        };

     
        
        
        await channel.BasicConsumeAsync(
            queue: "SelamKuyruk",
            autoAck: false,
            consumer: consumer
        );

        // 〽️ Programın kapanmaması için sonsuz bekleme
        await Task.Delay(-1); 
    }
    // TODO : Event muhabbetini kavrayip tekrar bakalım buna kendimiz yazamyi deneyelim
    
    // private static async Task OnMessageReceived(IModel channel, BasicDeliverEventArgs e)
    // {
    //         var body = e.Body.ToArray();
    //         var message = Encoding.UTF8.GetString(body);
    //         
    //         Console.WriteLine($" Alındı: {message}");
    //         
    //         await IModel
    // }
}