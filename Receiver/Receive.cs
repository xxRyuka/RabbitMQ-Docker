using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiver;

public class Receive
{
    public static async Task Main(string channelOpt)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "ryuka",
            Password = "123",
        };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: channelOpt,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        Console.WriteLine($" [*] {channelOpt} Kanalından Mesaj bekleniyor... Çıkmak için CTRL+C");

        var consumer = new AsyncEventingBasicConsumer(channel);

        // 1. ADIM: Fonksiyonu burada, Main'in içinde tanımlıyoruz.
        // channel degiskenini goremesi için aynı scope içinde local olarak tanımlıyorum fonksiyonu 
        async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"{message} mesajı alındı ,{channelOpt} kanalı dinleniliyor");
            Console.WriteLine($"{sender.GetType().FullName}");
            await channel.BasicAckAsync(ea.DeliveryTag, false);
        }


        // 2. ADIM: Event'e (olaya) '+= ' ile fonksiyonumuzun adını vererek abone oluyoruz.
        // Fonksiyonu çağırmadığımız için () parantezlerini kullanmıyoruz.
        
        // burdan (+= syntaxini ve Event Delegate mimarisini calışabilirsin) https://github.com/xxRyuka/Event-Delegate-Architecture-Example
        consumer.ReceivedAsync += OnMessageReceivedAsync;


        await channel.BasicConsumeAsync(
            queue: channelOpt,
            autoAck: false,
            consumer: consumer
        );

        // 〽️ Programın kapanmaması için sonsuz bekleme
        await Task.Delay(-1);
    }
    // xTODO Event muhabbetini kavrayip tekrar bakalım buna kendimiz yazamyi deneyelim Hallettik

    // bunun burda calısmıyor olmasını sebebi Closure (kapsam) durumundan otürü channel e erişemiyor
    // o sebeple fonksiyonu localde tanımlamamız gerekiyor 
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