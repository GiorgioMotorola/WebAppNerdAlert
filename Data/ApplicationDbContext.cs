using Microsoft.EntityFrameworkCore;
using WebAppNerdAlert.Models;

namespace WebAppNerdAlert.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
