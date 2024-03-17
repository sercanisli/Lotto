using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EntityFrameworkCore.Configuration
{
    public class WinningNumbersConfiguration : IEntityTypeConfiguration<WinningNumbers>
    {
        public void Configure(EntityTypeBuilder<WinningNumbers> builder)
        {
            builder.ToTable("WinningNumbers").HasKey(wn => wn.Id);
        }
    }
}
