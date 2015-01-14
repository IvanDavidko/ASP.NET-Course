
using System;
using System.Text.RegularExpressions;
using System.Web;

namespace PhotoManager.UI.Helpers
{
    public static class AlbumHelper
    {
        public static string CreateUrlFromTitle(string title)
        {
            var url = title;
            url = Regex.Replace(url, @"^\W+|\W+$", "");
            url = Regex.Replace(url, "'\"", "");
            url = Regex.Replace(url, @"_", "-");
            url = Regex.Replace(url, @"\W+", "-");

            return String.Format("{0}://{1}:{2}/album-by-title/{3}", HttpContext.Current.Request.Url.Scheme,
                HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port, url);
        }
    }
}