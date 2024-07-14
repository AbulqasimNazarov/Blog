using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Core.Topic.Data.Configurations;

using BlogApp.Core.Topic.Models;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Name)
            .IsRequired();
    }
}
