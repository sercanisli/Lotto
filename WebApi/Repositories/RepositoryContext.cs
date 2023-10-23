using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
