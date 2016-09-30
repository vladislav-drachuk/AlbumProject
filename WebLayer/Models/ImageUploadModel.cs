using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLayer.Models
{
    public class ImageUploadModel
    {
        [Required]
        public HttpPostedFileBase Image { get; set; }
        public string Description { get; set; }
    }
}