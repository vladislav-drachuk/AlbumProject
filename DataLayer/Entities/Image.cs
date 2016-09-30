using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AlbumProject.DataLayer.Entities
{
    public class Image
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual  ApplicationUser ApplicationUser { get; set; }

        public bool IsMain { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

        public Image()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
            Comments = new List<Comment>();
            Likes = new List<Like>();
            Description = "";
        }
    }
}
