using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Smoothboard_Stylers.Queues
{
    public interface ITaskQueue
    {
        ValueTask QueueAsync(Func<CancellationToken, ValueTask> workItem);
        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
        IAsyncEnumerable<Func<CancellationToken, ValueTask>> GetQueuedTasks();
    }
}
