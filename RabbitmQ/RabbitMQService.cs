using RabbitMQ.Client;
using System;

namespace RabbitmQ
{
    public class RabbitMQService
    {
        private readonly string _host = "localhost";
        private readonly string _username = "fkarahan";
        private readonly string _password = "3866";

        public IConnection Connection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                HostName = _host,
                UserName = _username,
                Password = _password,
            };
            return connectionFactory.CreateConnection();
        }

    }
}
