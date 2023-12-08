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
            );
        }
    }
}
