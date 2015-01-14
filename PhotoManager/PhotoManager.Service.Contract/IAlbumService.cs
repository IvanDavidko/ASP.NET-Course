
using System.ServiceModel;
using System.Threading.Tasks;
using PhotoManager.Service.Contract.DataContract;

namespace PhotoManager.Service.Contract
{
    [ServiceContract]
    public interface IAlbumService
    {
        [OperationContract]
        Task<AlbumResponse> GetAlbumById(int id);

        [OperationContract]
        Task<AlbumResponse[]> GetAlbumsByUserId(int userId);

        [OperationContract]
        Task<AlbumResponse> GetAlbumByUrl(string url);

        [OperationContract]
        Task<AlbumResponse[]> GetAllAlbums();

        [OperationContract]
        Task<int> InsertAlbum(AlbumResponse album);

        [OperationContract]
        Task<int> UpdateAlbum(AlbumResponse album);

        [OperationContract]
        Task<int> DeleteAlbumById(int id);

        [OperationContract]
        Task<int> AddPhotoToAlbum(int photoId, int albumId);

        [OperationContract]
        Task<int> ChooseTitlePhoto(int photoId, int albumId);
    }
}
