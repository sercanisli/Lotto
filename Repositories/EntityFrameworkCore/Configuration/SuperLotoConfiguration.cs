using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace Repositories.EntityFrameworkCore.Configuration
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
        }
    }
}
