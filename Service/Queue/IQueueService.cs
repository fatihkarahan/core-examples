using System;
namespace Service.Queue
{
    using System.Collections.Generic;
    using System.Text;
    using Data.Entities;
    public interface IQueueService
    {
        void AddQueue(Queue queue);
        List<Queue> GetQueue(string QueueName);

    }
}
