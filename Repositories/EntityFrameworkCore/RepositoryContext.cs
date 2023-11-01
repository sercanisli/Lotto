using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EntityFrameworkCore.Configuration;

namespace Repositories.EntityFrameworkCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public DbSet<SuperLoto> SuperLotos { get; set; }

        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new SuperLotoConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
