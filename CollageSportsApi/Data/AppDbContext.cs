using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CollageSportsApi.Models;

namespace CollageSportsApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Sports> Sports { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
