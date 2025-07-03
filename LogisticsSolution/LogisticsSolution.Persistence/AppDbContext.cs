using Microsoft.EntityFrameworkCore;
using System;

namespace LogisticsSolution.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Delivery> Deliveries { get; set; } = null!;
    }

    public class Delivery
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PickupAddress { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
