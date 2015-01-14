
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAlbumServiceRepository
    {
        Task<Album> GetAlbumById(int id);
        Task<Album[]> GetAlbumsByUserId(int userId);
        Task<Album> GetAlbumByUrl(string url);
        Task<Album[]> GetAllAlbums();
        Task<OperationResult<bool>> InsertAlbum(Album album);
        Task<OperationResult<bool>> UpdateAlbum(Album album);
        Task<int> DeleteAlbumById(int id);
        Task<int> AddPhotoToAlbum(int photoId, int albumId);
        Task<int> ChooseTitlePhoto(int photoId, int albumId);
    }
}
