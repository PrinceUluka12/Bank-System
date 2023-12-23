using BankSystem.CustomerInformation.Models;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using BankSystem.CustomerInformation.Services.Interfaces;

namespace BankSystem.CustomerInformation.Services.Implementations
{
    public class PublishMessages : IPublishMessages
    {
        public  void SendEmail(Notifications notifications)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "BankService",
                Password = "12345678",
                VirtualHost = "/"
            };
            //Rabbit Mq create Connection
            var conn = factory.CreateConnection();

            // Channel for Rabbit Mq
            using var channel = conn.CreateModel();

            // Create Queue

            channel.QueueDeclare("banknotifications", durable: true, exclusive: false);

            // convert to byte array to be sent

            var jsonString = JsonSerializer.Serialize(notifications);
            var body = Encoding.UTF8.GetBytes(jsonString);

            // Send Message on RabbitMQ
            channel.BasicPublish("", "banknotifications", body: body);

        }
    }
}
