using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EntityFrameworkCore.Configuration
{
    public class SansTopuGetRandomLogsConfiguration : IEntityTypeConfiguration<SansTopuGetRandomLogs>
    {
        public void Configure(EntityTypeBuilder<SansTopuGetRandomLogs> builder)
        {
            builder.ToTable("SansTopuGetRandomLogs").HasKey(s => s.Id);
            builder.Property(s => s.RandomNumbers)
                .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            );
        }
    }
}
