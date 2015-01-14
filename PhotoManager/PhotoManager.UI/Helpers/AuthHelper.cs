using System;
using System.Globalization;
using System.Web;
using System.Web.Security;
using Domain.Models;

namespace PhotoManager.UI.Helpers
{
    public static class AuthHelper
    {
        public static void LogIn(HttpContextBase httpContext, User user)
        {
            RoleHelper.PutUserRole(user);
            FormsAuthentication.SetAuthCookie(user.Id.ToString(CultureInfo.InvariantCulture), true);
        }

        public static void LogOut(HttpContextBase httpContext)
        {
            RoleHelper.ClearSession();
            FormsAuthentication.SignOut();
        }

        public static int GetUserIdFromCookie(HttpContextBase httpContext)
        {
            int id;
            return Int32.TryParse(httpContext.User.Identity.Name, out id) ? id : 0;
        }
    }
}