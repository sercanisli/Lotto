using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EntityFrameworkCore.Configuration
{
    public class OnNumaraConfiguration : IEntityTypeConfiguration<OnNumara>
    {
        public void Configure(EntityTypeBuilder<OnNumara> builder)
        {
            builder.ToTable("OnNumaras").HasKey(s => s.Id);
            builder.Property(s => s.Numbers)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            )
            .HasColumnName("Numbers");
            builder.HasData(
                new OnNumara
                {
                    Id = 1,
                    Numbers = new List<int> { 6, 25, 17, 13, 27, 60, 63, 66, 71, 78 },
                    Date = new DateTime(2022, 11, 27)
                },
                new OnNumara
                {
                    Id = 2,
                    Numbers = new List<int> { 19, 17, 9, 23, 27, 45, 47, 53, 64, 67 },
                    Date = new DateTime(2023, 9, 16)
                },
                new OnNumara
                {
                    Id = 3,
                    Numbers = new List<int> { 13, 45, 53, 52, 27, 3, 34, 37, 59, 78 },
                    Date = new DateTime(2023, 12, 27)
                }
                );
        }
    }
}
