using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AlbumProject.DataLayer.Entities
{
    public class Like
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("Image")]
        public string ImageId { get; set; }
        public virtual Image Image { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime Date { get; set; }
        public Like()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }


    }
}
