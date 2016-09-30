using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlbumProject.BusinessLogicLayer.DataTransferObjects
{
    public class CommentDTO
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public ImageDTO Image { get; set; }
        public string ImageId { get; set; }

        public UserProfileDTO CommentedUser { get; set; }
        public string CommentedUserId { get; set; }

        public DateTime Date { get; set; }

        public CommentDTO()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
         
        }
    }
}
