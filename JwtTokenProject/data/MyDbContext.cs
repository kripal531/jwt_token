using JwtTokenProject.Entity;
using Microsoft.EntityFrameworkCore;

namespace JwtTokenProject.data
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
