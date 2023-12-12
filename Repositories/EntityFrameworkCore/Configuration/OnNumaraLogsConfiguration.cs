using Entities.LogModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EntityFrameworkCore.Configuration
{
    public class OnNumaraLogsConfiguration : IEntityTypeConfiguration<OnNumaraLogs>
    {
        public void Configure(EntityTypeBuilder<OnNumaraLogs> builder)
        {
            builder.ToTable("OnNumaraLogs").HasKey(s => s.Id);
            builder.Property(s => s.RandomNumbers)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            )
            .HasColumnName("RandomNumbers");
        }
    }
}
