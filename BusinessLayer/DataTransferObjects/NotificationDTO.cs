using System;

namespace AlbumProject.BusinessLogicLayer.DataTransferObjects
{
    public class NotificationDTO
    {
        public string Id { get; set; }

        public UserProfileDTO User { get; set; }
      

        public bool IsReaded { get; set; }

        public DateTime Date { get; set; }

        public bool IsLike { get; set; }
        
        public LikeDTO Like { get; set; }

        public bool IsComment { get; set; }

        public CommentDTO Comment { get; set; }
        public bool IsFollow { get; set; }
        public UserProfileDTO FollowerUser { get; set; }

        public NotificationDTO()
        {
            Id = new Guid().ToString();
            Date = DateTime.Now;
        }
    }
}