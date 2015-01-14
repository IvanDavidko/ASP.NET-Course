using System;
using System.Configuration;
using System.Web.Mvc;
using Core.Mapping;
using Domain.Enums;
using Domain.Interfaces;
using PhotoManager.Repository.ServiceRepository;
using PhotoManager.UI.Helpers;

namespace PhotoManager.UI.Filters
{
    public class PermissionToAddPhotoFilter : ActionFilterAttribute
    {
        public IUserServiceRepository UserServiceRepository { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UserServiceRepository = new UserServiceRepository(new UtilityMapper());

            if (filterContext.HttpContext.Request.IsAuthenticated && RoleHelper.UserRole() != RoleType.Premium)
            {
                var photoCount = UserServiceRepository.GetUserPhotoCount(AuthHelper.GetUserIdFromCookie(filterContext.HttpContext));

                int maxPhotoCount;
                if (!Int32.TryParse(ConfigurationManager.AppSettings["MaxPhotoCount"], out maxPhotoCount) &&
                    photoCount > maxPhotoCount)
                {
                    filterContext.ActionParameters["error"] = "Sorry, you can't add more photos.";
                    filterContext.Result = new RedirectResult("~/");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}