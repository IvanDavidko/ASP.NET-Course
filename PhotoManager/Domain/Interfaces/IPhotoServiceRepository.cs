using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPhotoServiceRepository
    {
        Task<Photo> GetPhotoById(int id);
        Task<Photo> GetTitlePhoto(int albumId);
        Task<int> MakePhotoTitle(int albumId, int photoId);
        Task<int[]> GetPhotosByAlbumId(int albumId);
        Task<int[]> GetPhotosByAlbumUrl(string albumUrl);
        Task<int[]> GetPhotosByUserId(int userId);
        Task<OperationResult<bool>> InsertPhoto(Photo photo);
        Task<OperationResult<bool>> UpdatePhoto(Photo photo);
        Task<int> DeletePhotoById(int id);
        Task<int> DeletePhotosByAlbumId(int albumId);
        Task<int> DeletePhotoFromAlbum(int albumId, int photoId);
        Task<List<int>> SearchPhoto(string name);
        Task<List<int>> ExtendedSearchPhoto(ExtendedSearch model);
    }
}
