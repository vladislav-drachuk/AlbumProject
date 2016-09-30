using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLayer.Models
{
    public class ImageModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public int CountOfLikes { get; set; }
        public bool IsLiked { get; set; }
        public string Description { get; set; }
    }
}