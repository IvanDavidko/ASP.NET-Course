
using System.Threading.Tasks;
using PhotoManager.Service.Data.Model;

namespace PhotoManager.Service.Data.RepositoryInterface
{
    public interface IAlbumRepository
    {
        Task<Album> GetAlbumById(int id);
        Task<Album[]> GetAlbumsByUserId(int userId);
        Task<Album> GetAlbumByUrl(string url);
        Task<Album[]> GetAllAlbums();
        Task<int> InsertAlbum(Album album);
        Task<int> UpdateAlbum(Album album);
        Task<int> DeleteAlbumById(int id);
        Task<int> AddPhotoToAlbum(int photoId, int albumId);
        Task<int> ChooseTitlePhoto(int photoId, int albumId);
    }
}
