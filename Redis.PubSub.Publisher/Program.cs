// See https://aka.ms/new-console-template for more information

using StackExchange.Redis;

ConnectionMultiplexer connection = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber = connection.GetSubscriber();

while (true)
{
    Console.Write("message: ");
    string message = Console.ReadLine();

    long clientNumber = await subscriber.PublishAsync("mychannel", message);

    //using pattern-matching
    //long clientNumber = await subscriber.PublishAsync("mychannel.x", message);
    Console.WriteLine(clientNumber + " clients received the message");
}