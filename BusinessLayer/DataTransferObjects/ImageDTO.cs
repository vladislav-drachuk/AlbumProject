using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumProject.BusinessLogicLayer.DataTransferObjects
{
    public class ImageDTO
    {
        public string Id { get; set; }

        public string Url { get; set; }
        public string UserId { get; set; }
        public UserProfileDTO User { get; set; }

        public bool IsMain { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<LikeDTO> Likes { get; set; }

        public ImageDTO()
        {
            
            Comments = new List<CommentDTO>();
            Likes = new List<LikeDTO>();
        }
    }
}
