
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserServiceRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        int GetUserAlbumCount(int userId);
        int GetUserPhotoCount(int userId);
        Task<int> InsertUser(string name, string password, string passwordSalt, string roleName);
        Task<int> UpdateUser(int id, string name, string password);
        Task<int> DeleteUser(int id);
        Task<int> UpdateUserRole(int userId, string roleName);
        Task<Role> GetUserRole(int userId);
    }
}
