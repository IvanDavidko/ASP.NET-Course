using System;
using System.Threading.Tasks;
using AutoMapper;
using PhotoManager.Service.Contract;
using PhotoManager.Service.Contract.DataContract;
using PhotoManager.Service.Data.Model;
using PhotoManager.Service.Data.Repository;
using PhotoManager.Service.Data.RepositoryInterface;
using PhotoManager.Service.Utilities;

namespace PhotoManager.Service
{
    public class UserService : PhotoManagerServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UtilityMapper _utilityMapper;

        public UserService()
        {
            _userRepository = new UserRepository();
            _utilityMapper = new UtilityMapper();
            MapperSetup();
        }

        private static void MapperSetup()
        {
            Mapper.CreateMap<User, UserResponse>()
                .ForSourceMember(c => c.Photos, option => option.Ignore())
                .ForSourceMember(c => c.Albums, option => option.Ignore());
            Mapper.CreateMap<Role, RoleResponse>()
                .ForSourceMember(c => c.Users, option => option.Ignore());
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            try
            {
                if (id < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                var user = await _userRepository.GetUserById(id);

                return user != null ? _utilityMapper.ObjectMapper<UserResponse, User>(user) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserResponse> GetUserByName(string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    throw new ArgumentException(_USER_NAME_INVALID_EXCEPTION);

                var user = await _userRepository.GetUserByName(name);

                return user != null ? _utilityMapper.ObjectMapper<UserResponse, User>(user) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetUserAlbumCount(int userId)
        {
            try
            {
                if (userId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                return _userRepository.GetUserAlbumCount(userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetUserPhotoCount(int userId)
        {
            try
            {
                if (userId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                return _userRepository.GetUserPhotoCount(userId);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<int> InsertUser(string name, string password, string passwordSalt, string roleName)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                    throw new ArgumentException(_USER_NAME_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(password))
                    throw new ArgumentException(_USER_PASSWORD_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(passwordSalt))
                    throw new ArgumentException(_USER_PASSWORD_SALT_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(roleName))
                    throw new ArgumentException(_ROLE_NAME_INVALID_EXCEPTION);

                return await _userRepository.InsertUser( name,  password,  passwordSalt,  roleName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateUser(int id, string name, string password)
        {
            try
            {
                if (id < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(name))
                    throw new ArgumentException(_USER_NAME_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(password))
                    throw new ArgumentException(_USER_PASSWORD_INVALID_EXCEPTION);

                return await _userRepository.UpdateUser(new User()
                {
                    Id = id,
                    Name = name,
                    Password = password
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> DeleteUser(int id)
        {
            try
            {
                if (id < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                return await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> UpdateUserRole(int userId, string roleName)
        {
            try
            {
                if (userId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);
                if (String.IsNullOrEmpty(roleName))
                    throw new ArgumentException(_ROLE_NAME_INVALID_EXCEPTION);

                return await _userRepository.UpdateUserRole(userId, roleName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RoleResponse> GetUserRole(int userId)
        {
            try
            {
                if (userId < 1)
                    throw new ArgumentException(_USERID_INVALID_EXCEPTION);

                var role = await _userRepository.GetUserRole(userId);

                return role != null ? _utilityMapper.ObjectMapper<RoleResponse, Role>(role) : null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
