

using ClipShare.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClipShare.DataAccess.Data.Config
{
    public class LikeDislikeConfig : IEntityTypeConfiguration<LikeDislike>
    {
        public void Configure(EntityTypeBuilder<LikeDislike> builder)
        {
            // defining primary key....
            builder.HasKey(x => new { x.AppUserId, x.VideoId });
            builder.HasOne(a => a.AppUser)
                .WithMany(c => c.LikeDislikes)
                .HasForeignKey(c => c.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.Video)
                .WithMany(c => c.LikeDislikes)
                .HasForeignKey(c => c.VideoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
