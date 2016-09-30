using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace WebLayer.Helpers
{
    public static class MvcExtensions
    {
        // (?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)
        static readonly string hashTagPattern = @"(?:(?<=\s)|^)#(\w*[A-Za-z_]+\w*)";
        public static string DescriptionWithHashTags(this HtmlHelper helper, string text)
        {
            return FormatDescriptionText(text);
        }

        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

        public static string FormatDescriptionText(string text)
        {
            if (text != null)
            {
                string result = text;

                return Regex.Replace(result, hashTagPattern, delegate (Match match)
                {
                    string v = match.ToString();
                   
                    return @"<a href=""#"" class=""hashtag"">"+v+"</a>";
                });
            }
            else return "";
        }
    }
}