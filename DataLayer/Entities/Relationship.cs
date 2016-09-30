using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AlbumProject.DataLayer.Entities
{
    public class Relationship
    {
        [Key]
        public string Id { get; set; }

        public string FollowerUserId { get; set; }
        public virtual ApplicationUser FollowerUser { get; set; }

        public string FollowingUserId { get; set; }
        public virtual ApplicationUser FollowingUser { get; set; }

        public DateTime Date { get; set; }

        public Relationship()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }
    }
}
