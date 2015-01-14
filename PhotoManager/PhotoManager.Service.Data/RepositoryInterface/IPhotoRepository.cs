
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PhotoManager.Service.Data.Model;

namespace PhotoManager.Service.Data.RepositoryInterface
{
    public interface IPhotoRepository
    {
        Task<Photo> GetPhotoById(int id);
        Task<Photo> GetTitlePhoto(int albumId);
        Task<int> MakePhotoTitle(int albumId, int photoId);
        Task<int[]> GetPhotosByAlbumId(int albumId);
        Task<int[]> GetPhotosByAlbumUrl(string albumUrl);
        Task<int[]> GetPhotosByUserId(int userId);
        Task<int> InsertPhoto(Photo photo);
        Task<int> UpdatePhoto(Photo photo);
        Task<int> DeletePhotoById(int id);
        Task<int> DeletePhotosByAlbumId(int albumId);
        Task<int> DeletePhotoFromAlbum(int albumId, int photoId);
        Task<List<int>> SearchPhoto(string name);
        Task<List<int>> ExtendedSearchPhoto(IEnumerable<Expression<Func<Photo, bool>>> filters);
    }
}
