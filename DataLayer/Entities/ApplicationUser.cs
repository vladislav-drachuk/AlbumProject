using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbumProject.DataLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Description { get; set; }
    
        public virtual ICollection<Image> Images { get; set; }

        [InverseProperty("FollowerUser")]
        public virtual ICollection<Relationship> Followers { get; set; }

        [InverseProperty("FollowingUser")]
        public virtual ICollection<Relationship> Followings { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public ApplicationUser()
        {
            Description = "";
            Likes = new List<Like>();
            Comments = new List<Comment>();
            Notifications = new List<Notification>();
            Followers = new List<Relationship>();
            Followings = new List<Relationship>();
           
        }

    }
}
