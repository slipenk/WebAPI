using Microsoft.EntityFrameworkCore;

namespace Lab_7_kpz
{
    public class DataContext : DbContext
    {
        public DbSet<Clients> Clients { get; set; }

        public DbSet<Entertainment> Entertainments { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
