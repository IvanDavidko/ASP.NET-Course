using PhotoManager.Service.Data.Model;
using System.Threading.Tasks;

namespace PhotoManager.Service.Data.RepositoryInterface
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task<int> InsertUser(string name, string password, string passwordSalt, string roleName);
        Task<int> UpdateUser(User userInfo);
        Task<int> DeleteUser(int id);

        Task<Role> GetUserRole(int userId);
        Task<int> UpdateUserRole(int userId, string roleName);

        int GetUserAlbumCount(int userId);
        int GetUserPhotoCount(int userId);
    }
}
