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
        }
    }
}
