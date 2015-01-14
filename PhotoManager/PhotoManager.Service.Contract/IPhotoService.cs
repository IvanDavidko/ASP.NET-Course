using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using PhotoManager.Service.Contract.DataContract;

namespace PhotoManager.Service.Contract
{
    [ServiceContract]
    public interface IPhotoService
    {
        [OperationContract]
        Task<PhotoResponse> GetPhotoById(int id);

        [OperationContract]
        Task<PhotoResponse> GetTitlePhoto(int albumId);

        [OperationContract]
        Task<int> MakePhotoTitle(int albumId, int photoId);

        [OperationContract]
        Task<int[]> GetPhotosByAlbumId(int albumId);

        [OperationContract]
        Task<int[]> GetPhotosByAlbumUrl(string albumUrl);

        [OperationContract]
        Task<int[]> GetPhotosByUserId(int userId);

        [OperationContract]
        Task<int> InsertPhoto(PhotoResponse photo);

        [OperationContract]
        Task<int> UpdatePhoto(PhotoResponse photo);

        [OperationContract]
        Task<int> DeletePhotoById(int id);

        [OperationContract]
        Task<int> DeletePhotosByAlbumId(int albumId);

        [OperationContract]
        Task<int> DeletePhotoFromAlbum(int albumId, int photoId);

        [OperationContract]
        Task<List<int>> SearchPhoto(string name);

        [OperationContract]
        Task<List<int>> ExtendedSearchPhoto(PhotoResponse source);
    }
}
