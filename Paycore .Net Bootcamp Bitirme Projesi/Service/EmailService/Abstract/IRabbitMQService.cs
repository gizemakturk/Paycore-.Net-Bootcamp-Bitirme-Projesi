using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.EmailService.Abstract
{
    public interface IRabbitMQService
    {
        public void Publish(MailRequest mailRequest);
    }
}
