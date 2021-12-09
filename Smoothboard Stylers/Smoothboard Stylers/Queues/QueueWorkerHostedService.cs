using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Queues
{
    public class QueueWorkerHostedService : BackgroundService
    {
        public MailQueue MailQueue { get; }

        public QueueWorkerHostedService(MailQueue mailQueue)
        {
            MailQueue = mailQueue;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await QueueWorker(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }

        private async Task QueueWorker(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await MailQueue.DequeueAsync(stoppingToken);
                await workItem(stoppingToken);
            }
        }
    }
}
