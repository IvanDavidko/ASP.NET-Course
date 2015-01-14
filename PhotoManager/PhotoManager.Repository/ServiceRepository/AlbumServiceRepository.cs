
using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Core.Mapping;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using PhotoManager.Service.Contract;
using PhotoManager.Service.Contract.DataContract;

namespace PhotoManager.Repository.ServiceRepository
{
    public class AlbumServiceRepository: IAlbumServiceRepository
    {
        private readonly UtilityMapper _utilityMapper;
        private readonly IValidator<Album> _validator;
        private const string ServiceFailedStateException = "Service response is in a failed state";

        public AlbumServiceRepository(UtilityMapper utilityMapper,
            IValidator<Album> validator)
        {
            _utilityMapper = utilityMapper;
            _validator = validator;
        }

        #region Get
        public async Task<Album> GetAlbumById(int id)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetAlbumById(id);
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<Album, AlbumResponse>(response) : new Album();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new Album();
        }

        public async Task<Album[]> GetAlbumsByUserId(int userId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetAlbumsByUserId(userId);
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<Album[], AlbumResponse[]>(response) : new Album[0];
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new Album[0];
        }

        public async Task<Album> GetAlbumByUrl(string url)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetAlbumByUrl(url);
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<Album, AlbumResponse>(response) : new Album();
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new Album();
        }

        public async Task<Album[]> GetAllAlbums()
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.GetAllAlbums();
                ((ICommunicationObject)client).Close();

                return response != null ? _utilityMapper.ObjectMapper<Album[], AlbumResponse[]>(response) : new Album[0];
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new Album[0];
        }
        #endregion

        #region Add, Update, Delete
        public async Task<OperationResult<bool>> InsertAlbum(Album album)
        {
            var validationResult = _validator.Validate(album);

            if (!validationResult.IsValid)
            {
                return new OperationResult<bool>
                {
                    IsValidationError = true,
                    ValidationResult = validationResult
                };
            }

            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.InsertAlbum(_utilityMapper.ObjectMapper<AlbumResponse, Album>(album));
                ((ICommunicationObject)client).Close();

                return new OperationResult<bool>
                {
                    Result = Convert.ToBoolean(response),
                    IsValidationError = false,
                };
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new OperationResult<bool>
            {
                IsValidationError = true
            };
        }

        public async Task<OperationResult<bool>> UpdateAlbum(Album album)
        {
            var validationResult = _validator.Validate(album);

            if (!validationResult.IsValid)
            {
                return new OperationResult<bool>
                {
                    IsValidationError = true,
                    ValidationResult = validationResult
                };
            }

            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.UpdateAlbum(_utilityMapper.ObjectMapper<AlbumResponse, Album>(album));
                ((ICommunicationObject)client).Close();

                return new OperationResult<bool>
                {
                    Result = Convert.ToBoolean(response),
                    IsValidationError = false
                };
            }
            catch
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            return new OperationResult<bool>
            {
                IsValidationError = true
            };
        }

        public async Task<int> DeleteAlbumById(int id)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.DeleteAlbumById(id);
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

        public async Task<int> AddPhotoToAlbum(int photoId, int albumId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.AddPhotoToAlbum(photoId, albumId);
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

        public async Task<int> ChooseTitlePhoto(int photoId, int albumId)
        {
            var bpsServiceUrl = new EndpointAddress(@"net.tcp://localhost:10300/AlbumService");
            var _channelFactory = new ChannelFactory<IAlbumService>(new NetTcpBinding
            {
                Security = new NetTcpSecurity
                {
                    Mode = SecurityMode.None
                }
            }, bpsServiceUrl);

            IAlbumService client = null;
            try
            {
                client = _channelFactory.CreateChannel();
                var response = await client.ChooseTitlePhoto(photoId, albumId);
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
    }
}
