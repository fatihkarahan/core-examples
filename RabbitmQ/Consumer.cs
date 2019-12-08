using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitmQ
{
    public class Consumer
    {
        private readonly RabbitMQService _rabbitMQService;
        public Consumer(string queueName)
        {
            _rabbitMQService = new RabbitMQService();

            using (IConnection connection = _rabbitMQService.Connection())
            using (IModel channel = connection.CreateModel())
            {
                

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}
