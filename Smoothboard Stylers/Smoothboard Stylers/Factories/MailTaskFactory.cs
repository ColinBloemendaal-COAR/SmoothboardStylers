using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Smoothboard_Stylers.Data;
using Smoothboard_Stylers.Models;
using Smoothboard_Stylers.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Factories
{
    public class MailTaskFactory
    {
        private readonly MailQueue _mailQueue;
        private readonly SmtpClient _smtpClient;
        private readonly IServiceScopeFactory _scopeFactory;

        public MailTaskFactory(MailQueue mailQueue, IServiceScopeFactory scopeFactory, ILogger<MailTaskFactory> logger)
        {
            _mailQueue = mailQueue;
            _scopeFactory = scopeFactory;

            _smtpClient = new SmtpClient("mail.151970.ao-alkmaar.nl", 587);
            _smtpClient.Credentials = new NetworkCredential("s151970@151970.ao-alkmaar.nl", "SE8Xg8gg8");
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public async void CreateNewsletterTasks(Newsletter newsletter)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                Smoothboard_StylersContext context = scope.ServiceProvider.GetRequiredService<Smoothboard_StylersContext>();
                foreach (NewsletterSubscriber subscriber in context.NewsletterSubscribers)
                {
                    await _mailQueue.QueueAsync((token) => NewsletterMailTask(_smtpClient, newsletter, new MailAddress(subscriber.Email), token));
                }
            }
        }

        public async void CreateCustomMailTasks(string subject, string mailContent, MailAddress emailTo, bool isHtml)
        {
            await _mailQueue.QueueAsync((token) => CustomMailTask(_smtpClient, mailContent, subject, emailTo, isHtml, token));
        }


        public async ValueTask NewsletterMailTask(SmtpClient smtpClient, Newsletter newsletter, MailAddress emailTo, CancellationToken token)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("s151970@151970.ao-alkmaar.nl");

                mailMessage.Subject = newsletter.Subject;
                mailMessage.Body = newsletter.Text;
                mailMessage.IsBodyHtml = newsletter.IsHtml;

                mailMessage.To.Add(emailTo);
                await smtpClient.SendMailAsync(mailMessage, token);
            }
        }

        public async ValueTask CustomMailTask(SmtpClient smtpClient, string mailContent, string subject, MailAddress emailTo, bool isHtml, CancellationToken token)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("s151970@151970.ao-alkmaar.nl");

                mailMessage.Subject = subject;
                mailMessage.Body = mailContent;
                mailMessage.IsBodyHtml = isHtml;

                mailMessage.To.Add(emailTo);
                await smtpClient.SendMailAsync(mailMessage, token);
            }
        }
    }
}