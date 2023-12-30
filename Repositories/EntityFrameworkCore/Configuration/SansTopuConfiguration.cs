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
        }
    }
}
