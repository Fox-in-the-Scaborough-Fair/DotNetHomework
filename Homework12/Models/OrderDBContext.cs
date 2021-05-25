using Microsoft.EntityFrameworkCore;

namespace Homework12.Models
{
    public class OrderDBContext : DbContext
    {
        public OrderDBContext(DbContextOptions<OrderDBContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}