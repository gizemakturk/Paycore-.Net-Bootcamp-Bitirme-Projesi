﻿using Data.Model;
using Microsoft.Extensions.Options;
using Service.EmailService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.EmailService.Concrete
{
    public class MailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IRabbitMQService _rabbitMQService;
        public MailService(IOptions<MailSettings> mailSettings, IRabbitMQService rabbitMQService)
        {
            _mailSettings = mailSettings.Value;
            _rabbitMQService = rabbitMQService;
        }

        // Call RabitMQService and publish 
        public void SendEmailIntoQueue(MailRequest mailRequest)
        {
            var mail = new MailMessage();
            mail.Sender = new MailAddress(_mailSettings.Mail, "s");
            mail.To.Add(new MailAddress(mailRequest.ToEmail, "ss"));
            mail.Subject = mailRequest.Subject;
            mail.Body = mailRequest.Body;
            mail.From = new MailAddress(_mailSettings.Mail, "ss");
            mail.BodyEncoding = System.Text.Encoding.UTF8;

            try
            {
                _rabbitMQService.Publish(mailRequest);
            }
            catch (Exception e)
            {

                throw new Exception(message: e.Message);
            }

        }


    }
}
