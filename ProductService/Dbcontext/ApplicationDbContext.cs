using Microsoft.EntityFrameworkCore;
using ProductService.Model;

namespace ProductService.Dbcontext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}
