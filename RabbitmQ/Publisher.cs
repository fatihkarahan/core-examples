using Newtonsoft.Json;
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
            Person person = new Person() { Name = "Bora", SurName = "Kasmer", ID = 1, BirthDate = new DateTime(1978, 6, 3), Message = "İlgili aday yakınımdır :)" };
            using (IConnection connection = _rabbitMQService.Connection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: new Dictionary<string, object> { { "data", data } });

                string message = JsonConvert.SerializeObject(person);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
        public class Person
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string SurName { get; set; }
            public DateTime BirthDate { get; set; }
            public string Message { get; set; }
        }

    }
}
