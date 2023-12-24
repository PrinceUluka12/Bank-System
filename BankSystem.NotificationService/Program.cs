using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using BankSystem.NotificationService.Models;
using BankSystem.NotificationService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public class Program
{
    static void Main()
    {
         
        Console.WriteLine("Welcome to the Notification Service");
        //Rabbit Mq Connection parameters
        var factory = new ConnectionFactory()
        {
            HostName = "http://localhost:15672",
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

        // Create Consumer
        var consumer = new EventingBasicConsumer(channel);


        consumer.Received += (model, eventArgs) =>
        {
            // get Byte Array
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            try
            {
                Notifications notifications = JsonSerializer.Deserialize<Notifications>(message);
                Console.WriteLine("New Email request Recieved......");
                EmailService.SendEmail(notifications);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to send email {ex}");
            }
        };

        // Make the appliction listen for more messages
        channel.BasicConsume("banknotifications", true, consumer);

        Console.ReadKey();
    }

   
}