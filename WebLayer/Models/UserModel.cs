using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLayer.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
       
        public bool IsFollowing { get; set; }
        public int CountOfFollowers { get; set; }
        public int CountOfFollowings { get; set; }
        public ImageModel ProfileImage { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsAdmin { get; set; }

    }
}