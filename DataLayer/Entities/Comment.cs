using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AlbumProject.DataLayer.Entities
{
    public class Comment
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("Image")]
        public string ImageId { get; set; }
        public Image Image { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public Comment()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }
    }
}
