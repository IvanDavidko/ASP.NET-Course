using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Interfaces;
using Domain.Models;
using PhotoManager.UI.Filters;
using PhotoManager.UI.Helpers;

namespace PhotoManager.UI.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoServiceRepository _photoServiceRepository;

        public PhotoController(IPhotoServiceRepository photoServiceRepository)
        {
            _photoServiceRepository = photoServiceRepository;
        }

        [HttpGet]
        public async Task<ActionResult> PhotoListByUrl(string url)
        {
            return View("PhotoList", await _photoServiceRepository.GetPhotosByAlbumUrl(url));
        }

        [HttpGet]
        public async Task<ActionResult> PhotoList(int albumId)
        {
            ViewBag.AlbumId = albumId;
            return View("PhotoList", await _photoServiceRepository.GetPhotosByAlbumId(albumId));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> UserPhotoList(int albumId)
        {
            ViewBag.AlbumId = albumId;
            return View("UserPhotoList", await _photoServiceRepository.GetPhotosByAlbumId(albumId));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> UserPhotos()
        {
            return View("UserPhotoList", await _photoServiceRepository.GetPhotosByUserId(AuthHelper.GetUserIdFromCookie(HttpContext)));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetUserPhoto(int id, bool maketitlebutton)
        {
            ViewBag.MakeAlbumTitleAvailable = maketitlebutton;
            return PartialView("UserPhoto", await _photoServiceRepository.GetPhotoById(id));
        }
        
        [HttpGet]
        public async Task<ActionResult> GetTitlePhoto(int albumId)
        {
            return PartialView("Photo", await _photoServiceRepository.GetTitlePhoto(albumId));
        }

        [HttpGet]
        public async Task<ActionResult> GetPhoto(int id)
        {
            return PartialView("Photo", await _photoServiceRepository.GetPhotoById(id));
        }

        [HttpGet]
        public async Task<ActionResult> PhotoDetails(int id)
        {
            return View("PhotoDetails", await _photoServiceRepository.GetPhotoById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> MakePhotoTitle(int albumId, int photoId)
        {
            return Json(await _photoServiceRepository.MakePhotoTitle(albumId, photoId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AddPhotosToAlbum(int albumId)
        {
            if (albumId < 1) throw new ArgumentException("Album id is not valid");

            ViewBag.AlbumId = albumId;

            return View(await _photoServiceRepository.GetPhotosByUserId(AuthHelper.GetUserIdFromCookie(HttpContext)));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> DeletePhotoFromAlbum(int albumId, int photoId)
        {
            if (albumId < 0 || photoId < 0) return Json("Failed", JsonRequestBehavior.AllowGet);

            return Json(await _photoServiceRepository.DeletePhotoFromAlbum(albumId, photoId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 300, VaryByParam = "*")]
        public ActionResult PhotoForm(Photo model)
        {
            return View(model);
        }

        #region Add Update Remove

        [HttpGet]
        [Authorize]
        [PermissionToAddPhotoFilter]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [PermissionToAddPhotoFilter]
        public async Task<ActionResult> Add(Photo model)
        {
            if (!UploadPhotoHelper.GetData(HttpContext, model)) return View(model);

            model.UserId = AuthHelper.GetUserIdFromCookie(HttpContext);
            var response = await _photoServiceRepository.InsertPhoto(model);

            ViewBag.ErrorResult = response;
            if (response.IsValidationError) return View(model);

            //Photo is added successfully
            return RedirectToAction("AlbumList", "Album");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Update(int id)
        {
            return View(await _photoServiceRepository.GetPhotoById(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Photo model)
        {
            UploadPhotoHelper.GetData(HttpContext, model);


            var photoExist = await _photoServiceRepository.GetPhotoById(model.Id);
            if (photoExist.Image == null) return View(model);

            if (model.Image == null && String.IsNullOrEmpty(model.ImageSize) && String.IsNullOrEmpty(model.ImageType))
            {
                model.Image = photoExist.Image;
                model.ImageSize = photoExist.ImageSize;
                model.ImageType = photoExist.ImageType;
            }

            model.UserId = AuthHelper.GetUserIdFromCookie(HttpContext);
            var response = await _photoServiceRepository.UpdatePhoto(model);

            ViewBag.ErrorResult = response;
            if (response.IsValidationError)
                return View(model);

            //Photo is updated successfully
            return RedirectToAction("AlbumList", "Album");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Remove(int id)
        {
            return Json(await _photoServiceRepository.DeletePhotoById(id), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}