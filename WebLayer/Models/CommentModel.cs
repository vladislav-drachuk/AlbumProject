using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebLayer.Models
{
    public class CommentModel
    {
        
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string ImageId { get; set; }
        public string Date { get; set; }

    }
}