using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EntityFrameworkCore.Configuration;

namespace Repositories.EntityFrameworkCore
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
