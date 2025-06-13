using Microsoft.EntityFrameworkCore;
using OrderApi.model;

namespace OrderApi.data
{
    public class OrderDbContext :DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
    }
}
