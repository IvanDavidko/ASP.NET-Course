using System.ServiceModel;
using System.Threading.Tasks;
using Core.Mapping;
using Domain.Interfaces;
using Domain.Models;
using PhotoManager.Service.Contract;
using PhotoManager.Service.Contract.DataContract;

namespace PhotoManager.Repository.ServiceRepository
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly UtilityMapper _utilityMapper;

        public UserServiceRepository(UtilityMapper utilityMapper)
        {
            _utilityMapper = utilityMapper;
        }

        #region Get
        public async Task<User> GetUserById(int id)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetUserById(id);
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<User, UserResponse>(response) : new User();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new User();
        }

        public async Task<User> GetUserByName(string name)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetUserByName(name);
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<User, UserResponse>(response) : new User();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new User();
        }

        public int GetUserAlbumCount(int userId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = client.GetUserAlbumCount(userId);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }

        public int GetUserPhotoCount(int userId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = client.GetUserPhotoCount(userId);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }
        #endregion

        #region Add, Update, Delete
        public async Task<int> InsertUser(string name, string password, string passwordSalt, string roleName)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.InsertUser(name, password, passwordSalt, roleName);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }

        public async Task<int> UpdateUser(int id, string name, string password)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.UpdateUser(id, name, password);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }

        public async Task<int> DeleteUser(int id)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.DeleteUser(id);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }
        #endregion

        public async Task<int> UpdateUserRole(int userId, string roleName)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.UpdateUserRole(userId, roleName);
                ((ICommunicationObject)client).Close();

                return response;
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return 0;
        }

        public async Task<Role> GetUserRole(int userId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10100/UserService");
            var _channelFactory = new ChannelFactory<IUserService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IUserService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetUserRole(userId);
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<Role, RoleResponse>(response) : new Role();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new Role();
        }
    }
}
