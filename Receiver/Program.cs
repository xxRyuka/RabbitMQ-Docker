// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

char control = 's';
while (control !='q')
{
    Console.WriteLine("Hangi kanali dinlemek istiyorsunuz");
    string channel = Console.ReadLine();    
    
    Receiver.Receive.Main(channel);
    
}

Console.ReadLine();