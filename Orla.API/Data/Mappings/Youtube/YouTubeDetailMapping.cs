using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orla.Core.Models.Youtube;

namespace Orla.Api.Data.Mappings.Youtube;

public class YouTubeDetailMapping : IEntityTypeConfiguration<YouTubeDetail>
{
    public void Configure(EntityTypeBuilder<YouTubeDetail> builder)
    {
        builder.ToTable(name: "YouTubeDetail");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
                  .IsRequired(true)
                  .HasColumnType("NVARCHAR")
                  .HasMaxLength(200);

        builder.Property(x => x.Link)
                  .IsRequired(true)
                  .HasColumnType("NVARCHAR")
                  .HasMaxLength(200);

        builder.Property(x => x.Thumbnail)
                 .IsRequired(true)
                 .HasColumnType("NVARCHAR")
                 .HasMaxLength(200);

        builder.Property(x => x.PublishedAt)
          .IsRequired(true)
          .HasColumnType("DATETIME2");
        
    }
}
