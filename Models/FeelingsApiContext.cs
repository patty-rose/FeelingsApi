using Microsoft.EntityFrameworkCore;
using FeelingsApi.Models;

namespace FeelingsApi.Models
{
    public class FeelingsApiContext : DbContext
    {
        public FeelingsApiContext(DbContextOptions<FeelingsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Feeling> Feelings { get; set; }
    }
}