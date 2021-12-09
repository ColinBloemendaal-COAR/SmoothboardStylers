using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Queues
{
    public class MailQueue : ITaskQueue
    {
        private readonly Channel<Func<CancellationToken, ValueTask>> _queue;

        public MailQueue()
        {
            BoundedChannelOptions options = new BoundedChannelOptions(40)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<Func<CancellationToken, ValueTask>>(options);
        }

        public async ValueTask QueueAsync(Func<CancellationToken, ValueTask> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            await _queue.Writer.WriteAsync(workItem);
        }

        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
        {
            var workItem = await _queue.Reader.ReadAsync(cancellationToken);
            return workItem;
        }

        public IAsyncEnumerable<Func<CancellationToken, ValueTask>> GetQueuedTasks()
        {
            return _queue.Reader.ReadAllAsync();
        }
    }
}
