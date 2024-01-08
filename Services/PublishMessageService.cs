using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Services.Contracts;

namespace Services
{
    public class PublishMessageService : IPublishMessageService
    {
        public void EnqueueMessage<T>(T message)
        {
            var factory = new ConnectionFactory()
            {
                UserName = ConnectionFactory.DefaultUser,
                Password = ConnectionFactory.DefaultPass,
                Port = AmqpTcpEndpoint.UseDefaultPort,
                HostName = "localhost",
            };

            var conn = factory.CreateConnection();

           using var model = conn.CreateModel();


            //Properties of the Queue
            var queue = model.QueueDeclare("Test", durable: true, exclusive: false);

            //Convert message to byte array
            var token = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(token);

            //Writes bytes to the RabbitMQ Queue waiting to be dequeued
            model.BasicPublish("", "Test", body: body);
        }
    }
}
