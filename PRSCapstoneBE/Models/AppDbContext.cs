using Microsoft.EntityFrameworkCore;
using PRSCapstoneBE.Models;

namespace PRSCapstoneBE.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLine> RequestLines { get; set; }
        public DbSet<Po> Pos { get; set; }
        public DbSet<PoLine> PoLines { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) { }

        


    }
}
