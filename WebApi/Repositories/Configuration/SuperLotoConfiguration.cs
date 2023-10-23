using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Models;

namespace WebApi.Repositories.Configuration
{
    public class SuperLotoConfiguration : IEntityTypeConfiguration<SuperLoto>
    {
        public void Configure(EntityTypeBuilder<SuperLoto> builder)
        {
            builder.ToTable("SuperLotos").HasKey(s => s.Id);
            builder.Property(s => s.Numbers)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            )
            .HasColumnName("Numbers");
            builder.HasData(
                new SuperLoto
                {
                    Id = 1,
                    Numbers = new List<int> { 45,26,17,6,27,60}
                },
                new SuperLoto
                {
                    Id = 2,
                    Numbers = new List<int> { 25, 7, 9, 17, 27, 42 }
                },
                new SuperLoto
                {
                    Id = 3,
                    Numbers = new List<int> { 12, 4, 17, 6, 27, 60 }
                }
                );
        }
    }
}
