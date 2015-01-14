
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;
using Domain.Enums;
using Domain.Models;

namespace PhotoManager.UI.Helpers
{
    public static class RoleHelper
    {
        private const string CurrentRole = "CURRENT_ROLE";
        private static readonly HttpSessionState _session;

        static RoleHelper()
        {
            _session = HttpContext.Current.Session;
        }

        public static void PutUserRole(User user)
        {
            if (_session[CurrentRole] != null) return;

            _session[CurrentRole] = user.Role.Name;
        }

        public static RoleType UserRole()
        {
            if (_session[CurrentRole] != null && _session[CurrentRole].ToString() == RoleType.Premium.ToString())
                return RoleType.Premium;

            return RoleType.Base;
        }

        public static void ClearSession()
        {
            _session.Clear();
        }
    }
}