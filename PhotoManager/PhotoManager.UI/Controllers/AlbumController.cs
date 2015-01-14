using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Interfaces;
using Domain.Models;
using PhotoManager.UI.Filters;
using PhotoManager.UI.Helpers;

namespace PhotoManager.UI.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumServiceRepository _albumServiceRepository;

        public AlbumController(IAlbumServiceRepository albumServiceRepository)
        {
            _albumServiceRepository = albumServiceRepository;
        }

        public async Task<ActionResult> AlbumList(string error)
        {
            if (!String.IsNullOrEmpty(error))
                ViewBag.Error = error;

            return View(await _albumServiceRepository.GetAllAlbums());
        }

        [Authorize]
        public async Task<ActionResult> UserAlbumList()
        {
            return View(await _albumServiceRepository.GetAlbumsByUserId(AuthHelper.GetUserIdFromCookie(HttpContext)));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPhotosToAlbum(int albumId, int[] photoIds)
        {
            if (albumId <= 0 || photoIds.Count() <= 0) return Json("Failed", JsonRequestBehavior.AllowGet);

            foreach(var id in photoIds)
            {
                if (id <= 0) return Json("Failed", JsonRequestBehavior.AllowGet);

                await _albumServiceRepository.AddPhotoToAlbum(id, albumId);
            }

            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        #region Add Update Remove

        [HttpGet]
        [Authorize]
        [PermissionToAddAlbumFilter]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [PermissionToAddAlbumFilter]
        public async Task<ActionResult> Add(Album model)
        {
            if (String.IsNullOrEmpty(model.Title)) return View(model);

            model.Url = AlbumHelper.CreateUrlFromTitle(model.Title);

            var albumExist = await _albumServiceRepository.GetAlbumByUrl(model.Url);
            if (!String.IsNullOrEmpty(albumExist.Url)) return View(model);

            model.DateCreated = DateTimeOffset.Now;
            model.UserId = AuthHelper.GetUserIdFromCookie(HttpContext);
            var response = await _albumServiceRepository.InsertAlbum(model);

            ViewBag.ErrorResult = response;
            if (response.IsValidationError)
                return View(model);
            
            //Album is added successfully
            return RedirectToAction("AlbumList", "Album");
        }

        [HttpGet]
        [Authorize]
        [PermissionToAddAlbumFilter]
        public async Task<ActionResult> Update(int id)
        {
            return View(await _albumServiceRepository.GetAlbumById(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Album model)
        {
            if (String.IsNullOrEmpty(model.Title)) return View(model);

            model.Url = AlbumHelper.CreateUrlFromTitle(model.Title);

            var albumExist = await _albumServiceRepository.GetAlbumById(model.Id);
            if (String.IsNullOrEmpty(albumExist.Url)) return View(model);

            model.DateModified = DateTimeOffset.Now;
            model.UserId = AuthHelper.GetUserIdFromCookie(HttpContext);
            var response = await _albumServiceRepository.UpdateAlbum(model);

            ViewBag.ErrorResult = response;
            if (response.IsValidationError)
                return View(model);

            //Album is updated successfully
            return RedirectToAction("AlbumList", "Album");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Remove(int id)
        {
            return Json(await _albumServiceRepository.DeleteAlbumById(id), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}