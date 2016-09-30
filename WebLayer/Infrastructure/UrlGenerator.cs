using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 

namespace WebLayer.Infrastructure
{
    public static class UrlGenerator
    {
        public static string GenerateUrl(string userName)
        {
            Guid guid = new Guid();
            string url = Convert.ToBase64String(guid.ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", "");
            return userName + url;
        }
    }
}