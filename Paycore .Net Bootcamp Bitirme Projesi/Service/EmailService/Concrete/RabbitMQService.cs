using Data.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Service.EmailService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Service.EmailService.Concrete
{
    public class RabbitMQService : IRabbitMQService
    {
        private IConfiguration _configuration;
        public ConnectionFactory _factory { get; private set; }
        public RabbitMQService(IConfiguration configuration)
        {
            _configuration = configuration;
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void Publish(MailRequest mailRequest)
        {

            IConnection connection = _factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: "Mails",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mailRequest, formatting: Formatting.Indented));

            channel.BasicPublish(exchange: "",
                 routingKey: "Mails",
                 basicProperties: null,
                 body: body);

        }
    }
}
