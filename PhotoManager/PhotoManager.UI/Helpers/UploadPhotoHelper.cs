using System.IO;
using System.Web;
using Domain.Models;

namespace PhotoManager.UI.Helpers
{
    public static class UploadPhotoHelper
    {
        public static bool GetData(HttpContextBase httpContext, Photo model)
        {
            HttpPostedFileBase file = null;
            if (httpContext.Request.Files != null && httpContext.Request.Files.Count > 0)
            {
                file = httpContext.Request.Files[0];
            }

            if (file == null) return false;

            if (file.ContentType.ToLower() != "image/jpeg" &&
                file.ContentType.ToLower() != "image/png" &&
                file.ContentType.ToLower() != "image/jpg") return false;

            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                imageData = binaryReader.ReadBytes(file.ContentLength);
            }

            if (imageData.Length <= 0 || imageData.Length >= (1024 * 512)) return false;
            
            model.Image = imageData;
            model.ImageType = file.ContentType;
            model.ImageSize = imageData.Length.ToString();

            return true;
        }
    }
}