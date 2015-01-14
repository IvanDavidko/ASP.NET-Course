using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace PhotoManager.UI.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPhotoServiceRepository _photoServiceRepository;
        public SearchController(IPhotoServiceRepository photoServiceRepository)
        {
            _photoServiceRepository = photoServiceRepository;
        }

        [HttpGet]
        [OutputCache(Duration = 3600)]
        public ActionResult ExtendedSearch()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExtendedSearch(ExtendedSearch model)
        {
            return View("SearchResult", await _photoServiceRepository.ExtendedSearchPhoto(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(string searchName)
        {
            return View("SearchResult", await _photoServiceRepository.SearchPhoto(searchName));
        }
    }
}