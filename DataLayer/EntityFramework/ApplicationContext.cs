using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using AlbumProject.DataLayer.Entities;

namespace AlbumProject.DataLayer.EF
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }

        
        public DbSet<Image> Images { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet <Like> Likes { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Notification> Notifications { get; set; }
       


        /*  protected override void OnModelCreating(DbModelBuilder modelBuilder)
          {
              modelBuilder.Entity<Relationship>()
                          .HasRequired(r => r.Follower)
                          .WithMany(t => t.Followers)
                          .HasForeignKey(r => r.FollowerId)
                          .WillCascadeOnDelete(false);

              modelBuilder.Entity<Relationship>()
                          .HasRequired(r => r.Following)
                          .WithMany(t => t.Followings)
                          .HasForeignKey(r => r.FollowingId)
                          .WillCascadeOnDelete(false);
          }


      */


    }
}
