using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyGames.Core.User.Data.Configurations;

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
