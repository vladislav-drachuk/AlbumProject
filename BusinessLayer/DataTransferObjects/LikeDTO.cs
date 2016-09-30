using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumProject.BusinessLogicLayer.DataTransferObjects
{
    public class LikeDTO
    {
        public string Id { get; set; }

        public ImageDTO Image { get; set; }

        public string ImageId { get; set; }

        public UserProfileDTO LikedUser { get; set; }
        public string LikedUserId { get; set; }
        public DateTime Date { get; set; }

        public LikeDTO()
        {
            
        }
    }
}
