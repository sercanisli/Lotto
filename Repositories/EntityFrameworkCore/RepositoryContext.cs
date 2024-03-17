using Entities.LogModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Repositories.EntityFrameworkCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public DbSet<SuperLoto> SuperLotos { get; set; }
        public DbSet<SayisalLoto> SayisalLotos { get; set; }
        public DbSet<OnNumara> OnNumaras { get; set; }
        public DbSet<SansTopu> SansTopus { get; set; }
        public DbSet<WinningNumbers> WinningNumbers { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }

        public DbSet<OnNumaraLogs> OnNumaraLogs { get; set; }
        public DbSet<SansTopuLogs> SansTopuLogs { get; set; }
        public DbSet<SayisalLotoLogs> SayisalLotoLogs { get; set; }

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
