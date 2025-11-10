// See https://aka.ms/new-console-template for more information

using Sender;

Console.WriteLine("Hello, World!");

while (true)
{
    Console.Write("Mesaj gir (çıkmak için 'exit'): ");
    var input = Console.ReadLine();

    if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
        break; // kullanıcı 'exit' yazarsa çıkış

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Boş mesaj gönderilemez!");
        continue;
    }

    Send.Main(input); // (Girilen inputu Send.Main'e gönder)
}