using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using ShopOnline.Api.Services.Contracts;
using System.Text;

namespace ShopOnline.Api.Services
{
    public class RabbitMQProducer : IMessageProducer //, IDisposable
    {
        private readonly List<AmqpTcpEndpoint> amqpTcpEndpoints;
        private readonly ConnectionFactory factory;
        private IConnection _connection;
        private IModel channel;
        public RabbitMQProducer()
        {
            amqpTcpEndpoints = new List<AmqpTcpEndpoint>
            {
                new AmqpTcpEndpoint("rabbitmqnode1", 5672),
                new AmqpTcpEndpoint("rabbitmqnode2", 5673)
            };
            factory = new ConnectionFactory();
            _connection = Reconnect();
        }

        public void SendMessage<T>(T message)
        {
            if (!_connection.IsOpen)
            {
                _connection = Reconnect();
            }
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: "cart",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "cart", body: body);
        }

        private IConnection Reconnect()
        {
            try
            {
                return factory.CreateConnection(amqpTcpEndpoints);
            }
            catch
            {
                ConnectionFactory? factory = null;
                foreach (var endpoint in amqpTcpEndpoints)
                {
                    try
                    {
                        factory = new ConnectionFactory()
                        {
                            HostName = endpoint.HostName,
                            Port = endpoint.Port
                        };
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Failed to connect to host '{endpoint.HostName}' and port '{endpoint.Port}' ");
                    }
                }
                if (factory is not null)
                {
                    return factory.CreateConnection();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
