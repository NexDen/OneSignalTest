using Microsoft.EntityFrameworkCore;
using OneSignalTest.Models;
namespace OneSignalTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<OneSignalUser> OneSignalUsers { get; set; }
    }
    
}


