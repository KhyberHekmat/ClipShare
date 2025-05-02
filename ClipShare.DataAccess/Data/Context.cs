

using ClipShare.Core.Entities;
using ClipShare.DataAccess.Data.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ClipShare.DataAccess.Data
{
    public class Context:IdentityDbContext<AppUser,AppRole,int>
    {
        public Context(DbContextOptions<Context> options):base(options)
        {
            
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<Video> Video { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // shorter way of applying the manual configuration

            //$builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ApplyConfiguration(new CommentConfig());
            builder.ApplyConfiguration(new SubsribeConfig());
            builder.ApplyConfiguration(new LikeDislikeConfig());
        }
    }
}
