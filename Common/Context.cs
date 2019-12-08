using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Common
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Queue> Queues { get; set; }
    }
}
