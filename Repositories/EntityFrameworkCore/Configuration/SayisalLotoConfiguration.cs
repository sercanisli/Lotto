using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EntityFrameworkCore.Configuration
{
    public class SayisalLotoConfiguration : IEntityTypeConfiguration<SayisalLoto>
    {
        public void Configure(EntityTypeBuilder<SayisalLoto> builder)
        {
            builder.ToTable("SayisalLotos").HasKey(s => s.Id);
            builder.Property(s => s.Numbers)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            )
            .HasColumnName("Numbers");
            builder.HasData(
                new SayisalLoto
                {
                    Id = 1,
                    Numbers = new List<int> { 6, 25, 17, 13, 27, 60 },
                    Date = new DateTime(2022, 11, 27)
                },
                new SayisalLoto
                {
                    Id = 2,
                    Numbers = new List<int> { 19, 17, 9, 23, 27, 45 },
                    Date = new DateTime(2023, 9, 16)
                },
                new SayisalLoto
                {
                    Id = 3,
                    Numbers = new List<int> { 13, 45, 53, 52, 27, 3 },
                    Date = new DateTime(2023, 12, 27)
                }
                );
        }
    }
}
