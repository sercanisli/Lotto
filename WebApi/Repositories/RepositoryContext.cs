using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositories.Configuration;

namespace WebApi.Repositories
{
    public class RepositoryContext : DbContext
    {
        public DbSet<SuperLoto> SuperLotos { get; set; }

        public RepositoryContext(DbContextOptions options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SuperLotoConfiguration());
        }

    }
}
