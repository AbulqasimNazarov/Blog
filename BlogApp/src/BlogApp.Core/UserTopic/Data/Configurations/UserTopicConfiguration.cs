using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyGames.Core.UserGame.Data.Configurations;

using BlogApp.Core.UserTopic.Models;

public class UserGameConfiguration : IEntityTypeConfiguration<UserTopic>
{
    public void Configure(EntityTypeBuilder<UserTopic> builder)
    {
        builder
            .HasKey(ut => ut.Id);

        builder
            .HasOne(ut => ut.Topic)
            .WithMany(ut => ut.Users)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(ut => ut.User)
            .WithMany(ut => ut.Topics)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasIndex(ut => new {ut.UserId , ut.TopicId})
            .IsUnique();
    }
}
