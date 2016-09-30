using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumProject.BusinessLogicLayer.DataTransferObjects
{
    public class UserProfileDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }

        public ICollection<ImageDTO> Images { get; set; }
               
        public ICollection<UserProfileDTO> Followers { get; set; }
        
        public ICollection<UserProfileDTO> Followings { get; set; }

        public ICollection<LikeDTO> Likes { get; set; }
                
        public ICollection<NotificationDTO> Notifications { get; set; }

      
    }
}
