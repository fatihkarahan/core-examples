namespace Service.Queue
{
    using Common;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data.Entities;
    using System.Linq;

    public class QueueService : IQueueService
    {
        private readonly Context _dbContext;
        public QueueService(Context dbcontext)
        {
            _dbContext = dbcontext;
        }
        public void AddQueue(Queue queue) { _dbContext.Queues.Add(queue); _dbContext.SaveChanges(); }

        public List<Queue> GetQueue(string QueueName)
        {
           return _dbContext.Queues.Where(x => x.QueueName == QueueName).ToList();
        }
    }
}
