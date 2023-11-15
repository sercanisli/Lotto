using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EntityFrameworkCore.Configuration;
using System.Reflection;

namespace Repositories.EntityFrameworkCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public DbSet<SuperLoto> SuperLotos { get; set; }
        public DbSet<SayisalLoto> SayisalLotos { get; set; }
        public DbSet<OnNumara> OnNumaras { get; set; }

        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
