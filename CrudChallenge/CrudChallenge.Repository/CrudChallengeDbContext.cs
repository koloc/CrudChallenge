using CrudChallenge.Repository;
using Microsoft.EntityFrameworkCore;

namespace CrudChallenge.Data
{
    public class CrudChallengeDbContext : DbContext
    {
        public CrudChallengeDbContext(DbContextOptions<CrudChallengeDbContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
