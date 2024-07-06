using Microsoft.EntityFrameworkCore;
using USFWebAPI.Entities;

namespace USFWebAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Gender> Genders { get; set; }
    }
}
