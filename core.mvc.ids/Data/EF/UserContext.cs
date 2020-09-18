using core.mvc.ids.Models;
using Microsoft.EntityFrameworkCore;

namespace core.mvc.ids.Data.EF
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LOCALHOST\LOCALHOST;Database=Identities;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData
                (new User { Id = 1, Username = "farhan.qureshi", Password = "pragma1" },
                new User { Id = 2, Username = "fqureshi", Password = "pragma1" });
        }
    }
}