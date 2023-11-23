using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EntityFrameworkCore.Configuration
{
    public class SansTopuConfiguration : IEntityTypeConfiguration<SansTopu>
    {
        public void Configure(EntityTypeBuilder<SansTopu> builder)
        {
            builder.ToTable("SansTopus").HasKey(s => s.Id);
            builder.Property(s => s.Numbers)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            )
            .HasColumnName("Numbers");
            builder.HasData(
                new SansTopu
                {
                    Id = 1,
                    Numbers = new List<int> { 6, 25, 17, 13, 27},
                    PlusNumber = 4,
                    Date = new DateTime(2022, 11, 27)
                },
                new SansTopu
                {
                    Id = 2,
                    Numbers = new List<int> { 19, 17, 9, 5, 34},
                    PlusNumber = 9,
                    Date = new DateTime(2023, 9, 16)
                },
                new SansTopu
                {
                    Id = 3,
                    Numbers = new List<int> { 13, 12, 1, 27, 22 },
                    PlusNumber = 3,
                    Date = new DateTime(2023, 12, 27)
                }
                );
        }
    }
}
