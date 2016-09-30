using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace AlbumProject.DataLayer.Entities
{
    public class Notification
    {
        [Key]
        public string Id { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
    
        public virtual ApplicationUser ApplicationUser { get; set; }

        public bool IsReaded { get; set; }
        
        public DateTime Date { get; set; }

        public Notification()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }
        
    }


    public class LikeNotification
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("Like")]
        public string LikeId { get; set; }

        public Like Like { get; set; }
    }

    public class CommentNotification
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("Comment")]
        public string CommentId { get; set; }

        public Comment Comment { get; set; }
    }

    public class FollowNotification
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("Relationship")]
        public string RelationshipId { get; set; }

        public Relationship Relationship { get; set; }
    }
}
