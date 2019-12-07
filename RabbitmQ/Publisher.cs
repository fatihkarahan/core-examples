using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitmQ
{
    public class Publisher
    {
        private readonly RabbitMQService _rabbitMQService;
        public Publisher(string queueName, object data)
        {
            _rabbitMQService = new RabbitMQService();

            using (IConnection connection = _rabbitMQService.Connection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: new Dictionary<string, object> { { "data", data } });

            }
        }

    }
}
