using Entities.LogModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EntityFrameworkCore.Configuration
{
    public class SansTopuLogsConfiguration : IEntityTypeConfiguration<SansTopuLogs>
    {
        public void Configure(EntityTypeBuilder<SansTopuLogs> builder)
        {
            builder.ToTable("SansTopuLogs").HasKey(s => s.Id);
            builder.Property(s => s.RandomNumbers)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            )
            .HasColumnName("RandomNumbers");

            builder.HasData(
                new SansTopuLogs
                {
                    Id = 1,
                    UserName = "sercanisli",
                    RandomPlusNumber = 11,
                    RandomNumbers = new List<int> { 5,10,15,20,25},
                    Date = DateTime.Now
                },
                new SansTopuLogs
                {
                    Id = 2,
                    UserName = "esinduru",
                    RandomPlusNumber = 3,
                    RandomNumbers = new List<int> { 6, 7, 17, 21, 27, 16 },
                    Date = DateTime.Now
                }
                );
        }
    }
}
