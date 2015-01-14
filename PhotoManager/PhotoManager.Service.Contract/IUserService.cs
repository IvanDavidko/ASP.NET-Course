using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using PhotoManager.Service.Contract.DataContract;

namespace PhotoManager.Service.Contract
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Task<UserResponse> GetUserById(int id);

        [OperationContract]
        Task<UserResponse> GetUserByName(string name);

        [OperationContract]
        int GetUserAlbumCount(int userId);

        [OperationContract]
        int GetUserPhotoCount(int userId);

        [OperationContract]
        Task<int> InsertUser(string name, string password, string passwordSalt, string roleName);

        [OperationContract]
        Task<int> UpdateUser(int id, string name, string password);

        [OperationContract]
        Task<int> DeleteUser(int id);

        [OperationContract]
        Task<int> UpdateUserRole(int userId, string roleName);

        [OperationContract]
        Task<RoleResponse> GetUserRole(int userId);
    }
}
