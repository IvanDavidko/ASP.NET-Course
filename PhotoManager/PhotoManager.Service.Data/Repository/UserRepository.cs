using System.Data.Entity;
using PhotoManager.Service.Data.Model;
using PhotoManager.Service.Data.RepositoryInterface;
using System.Threading.Tasks;

namespace PhotoManager.Service.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserById(int id)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Users.Include("Role").FirstOrDefaultAsync(u => u.Id == id);
            }
        }

        public async Task<User> GetUserByName(string name)
        {
            using (var context = new PhotoManagerEntities())
            {
                return await context.Users.Include("Role").FirstOrDefaultAsync(u => u.Name == name);
            }
        }

        public async Task<int> InsertUser(string name, string password, string passwordSalt, string roleName)
        {
            using (var context = new PhotoManagerEntities())
            {
                var user = new User
                {
                    Name = name,
                    Password = password,
                    PasswordSalt = passwordSalt
                };

                var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                user.RoleId = role.Id;

                context.Users.Add(user);

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateUser(User userInfo)
        {
            using (var context = new PhotoManagerEntities())
            {
                var user = await context.Users.FindAsync(userInfo.Id);

                if (user == null) return 0;

                user.Name = userInfo.Name;
                user.Password = userInfo.Password;

                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteUser(int id)
        {
            using (var context = new PhotoManagerEntities())
            {
                var user = await context.Users.FindAsync(id);
                context.Users.Remove(user);

                return await context.SaveChangesAsync();
            }
        }

        public int GetUserAlbumCount(int userId)
        {
            using (var context = new PhotoManagerEntities())
            {
                var user = context.Users.Find(userId);

                return user != null ? user.Albums.Count : 0;
            }
        }

        public int GetUserPhotoCount(int userId)
        {
            using (var context = new PhotoManagerEntities())
            {
                var user = context.Users.Find(userId);

                return user != null ? user.Photos.Count : 0;
            }
        }

        public async Task<int> UpdateUserRole(int userId, string roleName)
        {
            using (var context = new PhotoManagerEntities())
            {
                var user = await context.Users.FindAsync(userId);
                var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                if (user == null || role == null) return 0;

                user.RoleId = role.Id;
                return await context.SaveChangesAsync();
            }
        }

        public async Task<Role> GetUserRole(int userId)
        {
            using (var context = new PhotoManagerEntities())
            {
                var user = await context.Users.Include("Role").FirstOrDefaultAsync(u => u.Id == userId);
                return user.Role;
            }
        }
    }
}
