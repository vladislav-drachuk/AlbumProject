using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AlbumProject.BusinessLogicLayer.DataTransferObjects
{
    public class RelationshipDTO
    {
        public string Id { get; set; }
        
        public string FollowerUserId { get; set; }
        
        
        public string FollowingUserId { get; set; }
        public DateTime Date { get; set; }

        public RelationshipDTO()
        {
            Id = Guid.NewGuid().ToString();
            Date = DateTime.Now;
        }

      
    }
}
