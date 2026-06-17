using Microsoft.EntityFrameworkCore;
using najnovijipokusajREST.Models;

namespace najnovijipokusajREST.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;

    }
}