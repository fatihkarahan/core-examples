using Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service.Queue;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitmQ
{
    public class Consumer
    {
        private readonly RabbitMQService _rabbitMQService;
        private readonly Context _dbContext;

        public Consumer(IQueueService _queueService,string queueName)
        {
            try
            {
                _rabbitMQService = new RabbitMQService();
                string message = string.Empty;
                using (IConnection connection = _rabbitMQService.Connection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        message = Encoding.UTF8.GetString(body);
                        Person person = JsonConvert.DeserializeObject<Person>(message);
                        Task.Run(() => { // or ThreadPool.QueueUserWorkItem(async _ => {
                            var builder = new DbContextOptionsBuilder<Context>();
                            var connectionString = "Server=.; Database=ExampleDb; Trusted_Connection = True;";
                            builder.UseSqlServer(connectionString);
                            using (var context = new Context(builder.Options))
                            {
                                context.Queues.Add(new Data.Entities.Queue() { Data = message, QueueName = queueName });
                                context.SaveChanges();
                            }
                        });
                        
                    };
                    channel.BasicConsume(queue: queueName,
                                         autoAck: true,
                                         consumer: consumer);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

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

