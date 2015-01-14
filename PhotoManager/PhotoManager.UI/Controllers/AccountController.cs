using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Enums;
using Domain.Interfaces;
using Elmah;
using PhotoManager.UI.Helpers;
using PhotoManager.UI.Models;

namespace PhotoManager.UI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserServiceRepository _userServiceRepository;

        public AccountController(IUserServiceRepository userServiceRepository)
        {
            _userServiceRepository = userServiceRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userServiceRepository.GetUserByName(model.Name);
            if (user == null) return View(model);

            if (SecurityHelper.CheckUserPassword(model.Password, user.PasswordSalt, user.Password))
            {
                AuthHelper.LogIn(HttpContext, user);
                return RedirectToAction("AlbumList", "Album");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        public ActionResult LogOut()
        {
            AuthHelper.LogOut(HttpContext);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var existingUser = await _userServiceRepository.GetUserByName(model.Name);
            if (existingUser.Id != 0)  return View(model);

            var user = SecurityHelper.CreateUser(model.Password);
            var response = await _userServiceRepository.InsertUser(model.Name, user.Password, user.PasswordSalt, RoleType.Base.ToString());

            if (response != (int)ResponseType.Fail)
                return RedirectToAction("AlbumList", "Album");

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Premium()
        {
            return Json(await _userServiceRepository.UpdateUserRole(AuthHelper.GetUserIdFromCookie(HttpContext), RoleType.Premium.ToString()), JsonRequestBehavior.AllowGet);
        }
    }
}