// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.Title = "Shop Online Cart item lisener";

SetConsoleHeader();
Console.WriteLine("\n\nPress any key to exit...");
var endPointList = new List<AmqpTcpEndpoint>
            {
                new AmqpTcpEndpoint("localhost", 5672),
                new AmqpTcpEndpoint("localhost", 5673)
            };
var factory = new ConnectionFactory();
using var connection = factory.CreateConnection(endPointList);
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: "cart",
                 durable: false,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    SetConsoleHeader();
    Console.WriteLine($"Message recived from '{eventArgs.RoutingKey}' channel.");
    Console.WriteLine("Message: "+message);
    Console.WriteLine("\n\nPress any key to exit...");
};
channel.BasicConsume(queue: "cart", autoAck: true, consumer: consumer);
Console.ReadKey(true);

static void SetConsoleHeader()
{
    Console.Clear();
    Console.WriteLine("Hello to shop online Cart Reader!");
    Console.WriteLine("\nHere you will find the log of new items added to carts a real time!");
}