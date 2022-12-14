
using Data.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MailSettings _mailSettings;
        public ConnectionFactory _factory { get; private set; }

        public Worker(ILogger<Worker> logger, IOptions<MailSettings> mailSettings)
        {
            _logger = logger;
            _mailSettings = mailSettings.Value;
            _factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                IConnection connection = _factory.CreateConnection();
                IModel channel = connection.CreateModel();

                MailMessage email;

                channel.QueueDeclare(queue: "Mails",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                var deneme = _mailSettings.Mail;
                consumer.Received += async (sender, e) => {
                    _logger.LogInformation($"Email: {Encoding.UTF8.GetString(e.Body.ToArray())}");

                    var data = JsonConvert.DeserializeObject<MailRequest>(Encoding.UTF8.GetString(e.Body.ToArray()));
                    email = new MailMessage() { Body = data.Body, From = new MailAddress("mail"), Subject = data.Subject, Sender = new MailAddress("mail") };
                    email.To.Add(new MailAddress(data.ToEmail));

                    await SendEmail(email);

                };

                channel.BasicConsume("Mails", true, consumer);


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
        public async Task SendEmail(MailMessage email)
        {
            using var smtp = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 587,
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential(
                "mail", "password"),
                EnableSsl = true
            };

            await smtp.SendMailAsync(email);

            smtp.Dispose();

        }


    }
}